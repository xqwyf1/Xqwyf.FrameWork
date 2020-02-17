using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.Auditing;

namespace Xqwyf.Application.Dtos
{
    [Serializable]
    public abstract class FullAuditedEntityDto : AuditedEntityDto, IFullAuditedObject
    {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTime? DeletionTime { get; set; }
    }
}
