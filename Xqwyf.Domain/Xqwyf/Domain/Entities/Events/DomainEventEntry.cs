using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Domain.Entities.Events
{
    /// <summary>
    /// 领域事件条目
    /// </summary>
    [Serializable]
    public class DomainEventEntry
    {
        /// <summary>
        /// 触发领域事件的实体
        /// </summary>
        public object SourceEntity { get; }

        /// <summary>
        /// 该事件的数据
        /// </summary>
        public object EventData { get; }

        public DomainEventEntry(object sourceEntity, object eventData)
        {
            SourceEntity = sourceEntity;
            EventData = eventData;
        }
    }
}
