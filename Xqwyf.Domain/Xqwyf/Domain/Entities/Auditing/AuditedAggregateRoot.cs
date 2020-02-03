using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.Auditing;

namespace Xqwyf.Domain.Entities.Auditing
{
    /// <summary>
    /// 具有基础审计属性的聚合根
    /// </summary>
    public abstract class AuditedAggregateRoot: AggregateRoot, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }

        /// <inheritdoc />
        public virtual DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? CreatorId { get; set; }
    }
}
