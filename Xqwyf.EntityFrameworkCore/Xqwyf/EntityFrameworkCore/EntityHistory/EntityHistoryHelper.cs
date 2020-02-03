using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using JetBrains.Annotations;

using Xqwyf.DependencyInjection;
using Xqwyf.Domain.Entities;
using Xqwyf.Auditing;
using Xqwyf.Timing;
using Xqwyf.Data;
using Xqwyf.MultiTenancy;
using Xqwyf.Json;


namespace Xqwyf.EntityFrameworkCore.EntityHistory
{
    public class EntityHistoryHelper : IEntityHistoryHelper, ITransientDependency
    {
        private readonly IClock _clock;

        /// <summary>
        /// 审计的配置信息
        /// </summary>
        protected XqAuditingOptions Options { get; }

        protected IJsonSerializer JsonSerializer { get; }

        public virtual List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries)
        {
            var list = new List<EntityChangeInfo>();

            foreach (var entry in entityEntries)
            {
                if (!ShouldSaveEntityHistory(entry))
                {
                    continue;
                }

                var entityChange = CreateEntityChangeOrNull(entry);
                if (entityChange == null)
                {
                    continue;
                }

                list.Add(entityChange);
            }

            return list;
        }

        /// <summary>
        /// 根据<paramref name="entityEntry"/>,创建一个<see cref="EntityChangeInfo"/>
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <returns></returns>
        [CanBeNull]
        private EntityChangeInfo CreateEntityChangeOrNull(EntityEntry entityEntry)
        {
            var entity = entityEntry.Entity;

            EntityChangeType changeType;
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    changeType = EntityChangeType.Created;
                    break;
                case EntityState.Deleted:
                    changeType = EntityChangeType.Deleted;
                    break;
                case EntityState.Modified:
                    changeType = IsDeleted(entityEntry) ? EntityChangeType.Deleted : EntityChangeType.Updated;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    return null;
            }

            var entityId = GetEntityId(entity);
            if (entityId == null && changeType != EntityChangeType.Created)
            {
                return null;
            }

            var entityType = entity.GetType();
            var entityChange = new EntityChangeInfo
            {
                ChangeType = changeType,
                EntityEntry = entityEntry,
                EntityId = entityId,
                EntityTypeFullName = entityType.FullName,
                PropertyChanges = GetPropertyChanges(entityEntry),
                EntityTenantId = GetTenantId(entity)
            };

            return entityChange;
        }


        protected virtual Guid? GetTenantId(object entity)
        {
            if (!(entity is IMultiTenant multiTenantEntity))
            {
                return null;
            }

            return multiTenantEntity.TenantId;
        }
        
        /// <summary>
        /// 获取实体当前的Id信息
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <returns></returns>
        private string GetEntityId(object entityAsObj)
        {
            if (!(entityAsObj is IEntity entity))
            {
                throw new XqException($"Entities should implement the {typeof(IEntity).AssemblyQualifiedName} interface! Given entity does not implement it: {entityAsObj.GetType().AssemblyQualifiedName}");
            }
            return (entity as Entity).GetID();
        }

        /// <summary>
        /// 判断是否应该保存实体的变更情况
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private bool ShouldSaveEntityHistory(EntityEntry entityEntry, bool defaultValue = false)
        {
            ///实体没有变更，或者实体不应该被记录，直接返回
            if (entityEntry.State == EntityState.Detached ||
                entityEntry.State == EntityState.Unchanged)
            {
                return false;
            }

            ///实体是否是可以不进行审计的类型
            if (Options.IgnoredTypes.Any(t => t.IsInstanceOfType(entityEntry.Entity)))
            {
                return false;
            }

            var entityType = entityEntry.Entity.GetType();

            ///当前对象不是实体
            if (!EntityHelper.IsEntity(entityType))
            {
                return false;
            }

            ///当前对象不是公共实体
            if (!entityType.IsPublic)
            {
                return false;
            }

            if (entityType.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (entityType.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            if (Options.EntityHistorySelectors.Any(selector => selector.Predicate(entityType)))
            {
                return true;
            }

            var properties = entityEntry.Metadata.GetProperties();
            if (properties.Any(p => p.PropertyInfo?.IsDefined(typeof(AuditedAttribute)) ?? false))
            {
                return true;
            }

            return defaultValue;
        }

        /// <summary>
        /// 判断是否应该保存属性的变更情况
        /// </summary>
        /// <param name="propertyEntry"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private bool ShouldSavePropertyHistory(PropertyEntry propertyEntry, bool defaultValue)
        {
            if (propertyEntry.Metadata.Name == "Id")
            {
                return false;
            }

            var propertyInfo = propertyEntry.Metadata.PropertyInfo;
            if (propertyInfo != null && propertyInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            var entityType = propertyEntry.EntityEntry.Entity.GetType();

            if (entityType.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                if (propertyInfo == null || !propertyInfo.IsDefined(typeof(AuditedAttribute), true))
                {
                    return false;
                }
            }

            var isModified = !(propertyEntry.OriginalValue?.Equals(propertyEntry.CurrentValue) ?? propertyEntry.CurrentValue == null);
            if (isModified)
            {
                return true;
            }

            return defaultValue;
        }

        /// <summary>
        /// 检查实体是否为新建实体
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <returns></returns>
        private bool IsCreated(EntityEntry entityEntry)
        {
            return entityEntry.State == EntityState.Added;
        }

        /// <summary>
        /// 检查实体是否为被删除实体,分为物理删除和逻辑删除处理
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <returns></returns>
        private bool IsDeleted(EntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                return true;
            }

            var entity = entityEntry.Entity;
            return entity is ISoftDelete && entity.As<ISoftDelete>().IsDeleted;
        }

        /// <summary>
        /// 获取<paramref name="entityEntry"/>中实体所有属性变更情况
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <returns></returns>
        private List<EntityPropertyChangeInfo> GetPropertyChanges(EntityEntry entityEntry)
        {
            var propertyChanges = new List<EntityPropertyChangeInfo>();
            var properties = entityEntry.Metadata.GetProperties();
            var isCreated = IsCreated(entityEntry);
            var isDeleted = IsDeleted(entityEntry);

            foreach (var property in properties)
            {
                var propertyEntry = entityEntry.Property(property.Name);
                if (ShouldSavePropertyHistory(propertyEntry, isCreated || isDeleted))
                {
                    propertyChanges.Add(new EntityPropertyChangeInfo
                    {
                        NewValue = isDeleted ? null : JsonSerializer.Serialize(propertyEntry.CurrentValue).TruncateWithPostfix(EntityPropertyChangeInfo.MaxValueLength),
                        OriginalValue = isCreated ? null : JsonSerializer.Serialize(propertyEntry.OriginalValue).TruncateWithPostfix(EntityPropertyChangeInfo.MaxValueLength),
                        PropertyName = property.Name,
                        PropertyTypeFullName = property.ClrType.GetFirstGenericArgumentIfNullable().FullName
                    });
                }
            }

            return propertyChanges;
        }


      
        public void UpdateChangeList(List<EntityChangeInfo> entityChanges)
        {
            foreach (var entityChange in entityChanges)
            {
                ///根据实体中的相关时间，设置变更时间
                entityChange.ChangeTime = GetChangeTime(entityChange);

            
                var entityEntry = entityChange.EntityEntry.As<EntityEntry>();
                entityChange.EntityId = GetEntityId(entityEntry.Entity);

             ///修改外键
                var foreignKeys = entityEntry.Metadata.GetForeignKeys();

                foreach (var foreignKey in foreignKeys)
                {
                    foreach (var property in foreignKey.Properties)
                    {
                        var propertyEntry = entityEntry.Property(property.Name);
                        var propertyChange = entityChange.PropertyChanges.FirstOrDefault(pc => pc.PropertyName == property.Name);

                        if (propertyChange == null)
                        {
                            if (!(propertyEntry.OriginalValue?.Equals(propertyEntry.CurrentValue) ?? propertyEntry.CurrentValue == null))
                            {
                                // Add foreign key
                                entityChange.PropertyChanges.Add(new EntityPropertyChangeInfo
                                {
                                    NewValue = JsonSerializer.Serialize(propertyEntry.CurrentValue),
                                    OriginalValue = JsonSerializer.Serialize(propertyEntry.OriginalValue),
                                    PropertyName = property.Name,
                                    PropertyTypeFullName = property.ClrType.GetFirstGenericArgumentIfNullable().FullName
                                });
                            }

                            continue;
                        }

                        if (propertyChange.OriginalValue == propertyChange.NewValue)
                        {
                            var newValue = JsonSerializer.Serialize(propertyEntry.CurrentValue);
                            if (newValue == propertyChange.NewValue)
                            {
                                // No change
                                entityChange.PropertyChanges.Remove(propertyChange);
                            }
                            else
                            {
                                // Update foreign key
                                propertyChange.NewValue = newValue.TruncateWithPostfix(EntityPropertyChangeInfo.MaxValueLength);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取实体的变更时间
        /// </summary>
        /// <param name="entityChange"></param>
        /// <returns></returns>
        private DateTime GetChangeTime(EntityChangeInfo entityChange)
        {
            var entity = entityChange.EntityEntry.As<EntityEntry>().Entity;
            switch (entityChange.ChangeType)
            {
                case EntityChangeType.Created:
                    return (entity as IAuditedObject)?.CreationTime ?? _clock.Now;
                case EntityChangeType.Deleted:
                    return (entity as IFullAuditedObject)?.DeletionTime ?? _clock.Now;
                case EntityChangeType.Updated:
                    return (entity as IAuditedObject)?.LastModificationTime ?? _clock.Now;
                default:
                    throw new XqException($"Unknown {nameof(EntityChangeInfo)}: {entityChange}");
            }
        }
    }
}
