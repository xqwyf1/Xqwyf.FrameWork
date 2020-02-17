using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.Auditing;

namespace Xqwyf.Application.Dtos
{
    public abstract class AuditedEntityDto : EntityDto, IAuditedObject
    {
        /// <inheritdoc />
        public DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public Guid? LastModifierId { get; set; }

        public DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public Guid? CreatorId { get; set; }
    }
}
