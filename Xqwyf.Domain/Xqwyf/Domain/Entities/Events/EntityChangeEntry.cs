using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain.Entities.Events
{
    /// <summary>
    /// 实体变更条目
    /// </summary>
    [Serializable]
    public class EntityChangeEntry
    {
        /// <summary>
        /// 被变更的实体
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// 创建一个实体变更记录
        /// </summary>
        /// <param name="entity">被变更的实体</param>
        /// <param name="changeType">被变更的类型</param>
        public EntityChangeEntry(object entity, EntityChangeType changeType)
        {
            Entity = entity;
            ChangeType = changeType;
        }
    }
}
