using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.Auditing;
namespace  Xqwyf.Domain.Entities.Auditing
{
    public abstract class FullAuditedEntity : AuditedEntity, IFullAuditedObject
    {
        /// <inheritdoc />
        public virtual bool IsDeleted { get; set; }

        /// <inheritdoc />
        public virtual Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? DeletionTime { get; set; }
    }
}
