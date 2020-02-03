using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Domain.Entities.Events
{
    /// <summary>
    /// 实体变更报告，用于实体变更后的通知
    /// </summary>
    public class EntityChangeReport
    {
        /// <summary>
        /// 发生变更的实体清单
        /// </summary>
        public List<EntityChangeEntry> ChangedEntities { get; }


        /// <summary>
        /// 触发的领域事件的清单
        /// </summary>
        public List<DomainEventEntry> DomainEvents { get; }


        /// <summary>
        /// 实体变更报告，用于实体变更后的通知
        /// </summary>
        public EntityChangeReport()
        {
            ChangedEntities = new List<EntityChangeEntry>();
            DomainEvents = new List<DomainEventEntry>();
            
        }

        public bool IsEmpty()
        {
            return ChangedEntities.Count <= 0 &&
                   DomainEvents.Count <= 0 ;
        }

        public override string ToString()
        {
            return $"[EntityChangeReport] ChangedEntities: {ChangedEntities.Count}, DomainEvents: {DomainEvents.Count}";
        }
    }
}
