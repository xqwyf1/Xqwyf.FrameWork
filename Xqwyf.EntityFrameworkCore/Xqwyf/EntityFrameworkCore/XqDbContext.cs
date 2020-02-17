using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Xqwyf.Data;

using Xqwyf.Guids;
using Xqwyf.Auditing;
using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Entities.Events;
using Xqwyf.EntityFrameworkCore.EntityHistory;

namespace Xqwyf.EntityFrameworkCore
{
    public abstract class XqDbContext<TDbContext> : DbContext, IEfCoreDbContext
         where TDbContext : DbContext
    {
        /// <summary>
        /// 实体变更情况记录帮助，可以创建、修改实体的变更列表
        /// </summary>
        public IEntityHistoryHelper EntityHistoryHelper { get; set; }

        public IAuditingManager AuditingManager { get; set; }

        public IAuditPropertySetter AuditPropertySetter { get; set; }

        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        public IGuidGenerator GuidGenerator { get; set; }


        public ILogger<XqDbContext<TDbContext>> Logger { get; set; }

        protected XqDbContext(DbContextOptions<TDbContext> options)
         : base(options)
        {
            GuidGenerator = SimpleGuidGenerator.Instance;
            EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            EntityHistoryHelper = NullEntityHistoryHelper.Instance;
            Logger = NullLogger<AbpDbContext<TDbContext>>.Instance;
        }

        /// <summary>
        /// 取消逻辑删除标记，变更为未删除
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }

        protected virtual void UpdateConcurrencyStamp(EntityEntry entry)
        {
            var entity = entry.Entity as IHasConcurrencyStamp;
            if (entity == null)
            {
                return;
            }

            Entry(entity).Property(x => x.ConcurrencyStamp).OriginalValue = entity.ConcurrencyStamp;
            entity.ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                var auditLog = AuditingManager?.Current?.Log;

                List<EntityChangeInfo> entityChangeList = null;

                if (auditLog != null)
                {
                    entityChangeList = EntityHistoryHelper.CreateChangeList(ChangeTracker.Entries().ToList());
                }

                var changeReport = ApplyXqConcepts();

                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);

                await EntityChangeEventHelper.TriggerEventsAsync(changeReport).ConfigureAwait(false);

                if (auditLog != null)
                {
                    EntityHistoryHelper.UpdateChangeList(entityChangeList);
                    auditLog.EntityChanges.AddRange(entityChangeList);
                    Logger.LogDebug($"Added {entityChangeList.Count} entity changes to the current audit log");
                }

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new XqDbConcurrencyException(ex.Message, ex);
            }
            finally
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        protected virtual EntityChangeReport ApplyXqConcepts()
        {
            var changeReport = new EntityChangeReport();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                ApplyXqConcepts(entry, changeReport);
            }

            return changeReport;
        }

        protected virtual void ApplyXqConcepts(EntityEntry entry, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyXqConceptsForAddedEntity(entry, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyXqConceptsForModifiedEntity(entry, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyXqConceptsForDeletedEntity(entry, changeReport);
                    break;
            }
            AddDomainEvents(changeReport, entry.Entity);
        }

        protected virtual void ApplyXqConceptsForAddedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            CheckAndSetId(entry);
            SetConcurrencyStampIfNull(entry);
            SetCreationAuditProperties(entry);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
        }
        protected virtual void ApplyXqConceptsForDeletedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            CancelDeletionForSoftDelete(entry);
            UpdateConcurrencyStamp(entry);
            SetDeletionAuditProperties(entry);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
        }

        protected virtual void ApplyXqConceptsForModifiedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            UpdateConcurrencyStamp(entry);
            SetModificationAuditProperties(entry);

            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry);
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
            }
            else
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Updated));
            }
        }



        /// <summary>
        /// 设置<paramref name="entry"/>的创建者，创建时间属性
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void SetCreationAuditProperties(EntityEntry entry)
        {
            AuditPropertySetter?.SetCreationProperties(entry.Entity);
        }

        protected virtual void SetModificationAuditProperties(EntityEntry entry)
        {
            AuditPropertySetter?.SetModificationProperties(entry.Entity);
        }

        protected virtual void SetDeletionAuditProperties(EntityEntry entry)
        {
            AuditPropertySetter?.SetDeletionProperties(entry.Entity);
        }

        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            if (entry.Entity is IEntity<Guid> entityWithGuidId)
            {
                TrySetGuidId(entry, entityWithGuidId);
            }
        }

        /// <summary>
        /// 增加领域事件，只用于聚合根
        /// </summary>
        /// <param name="changeReport"></param>
        /// <param name="entityAsObj"></param>
        protected virtual void AddDomainEvents(EntityChangeReport changeReport, object entityAsObj)
        {
            var generatesDomainEventsEntity = entityAsObj as IGeneratesDomainEvents;
            if (generatesDomainEventsEntity == null)
            {
                return;
            }

            var localEvents = generatesDomainEventsEntity.GetLocalEvents()?.ToArray();
            if (localEvents != null && localEvents.Any())
            {
                changeReport.DomainEvents.AddRange(localEvents.Select(eventData => new DomainEventEntry(entityAsObj, eventData)));
                generatesDomainEventsEntity.ClearLocalEvents();
            }
        }

        protected virtual void SetConcurrencyStampIfNull(EntityEntry entry)
        {
            var entity = entry.Entity as IHasConcurrencyStamp;
            if (entity == null)
            {
                return;
            }

            if (entity.ConcurrencyStamp != null)
            {
                return;
            }

            entity.ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

    }
}
