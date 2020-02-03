using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using Xqwyf.Data;



namespace Xqwyf.Auditing
{

    /// <summary>
    /// 记录实体的变更信息
    /// </summary>
    public class EntityChangeInfo : IHasExtraProperties
    {
        /// <summary>
        /// 实体变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 实体的变更类型
        /// </summary>
        public EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// TenantId of the related entity.
        /// This is not the TenantId of the audit log entry.
        /// There can be multiple tenant data changes in a single audit log entry.
        /// </summary>
        public Guid? EntityTenantId { get; set; }

        /// <summary>
        /// 实体的ID，如果为组合主键，记录的是组合主键的字符串
        /// </summary>
        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        /// <summary>
        /// 实体的属性变更记录
        /// </summary>
        public List<EntityPropertyChangeInfo> PropertyChanges { get; set; }

        public Dictionary<string, object> ExtraProperties { get; }

        public virtual object EntityEntry { get; set; } //TODO: Try to remove since it breaks serializability

        public EntityChangeInfo()
        {
            ExtraProperties = new Dictionary<string, object>();
        }

        public virtual void Merge(EntityChangeInfo changeInfo)
        {
            foreach (var propertyChange in changeInfo.PropertyChanges)
            {
                var existingChange = PropertyChanges.FirstOrDefault(p => p.PropertyName == propertyChange.PropertyName);
                if (existingChange == null)
                {
                    PropertyChanges.Add(propertyChange);
                }
                else
                {
                    existingChange.NewValue = propertyChange.NewValue;
                }
            }

            foreach (var extraProperty in changeInfo.ExtraProperties)
            {
                var key = extraProperty.Key;
                if (ExtraProperties.ContainsKey(key))
                {
                    key = InternalUtils.AddCounter(key);
                }

                ExtraProperties[key] = extraProperty.Value;
            }
        }
    }
}
