using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.Auditing;

namespace  Xqwyf.Domain.Entities.Auditing
{
    public abstract class AuditedEntityWithUser<TUser> : AuditedEntity, IAuditedObject<TUser>
       where TUser : IEntity
    {
        /// <inheritdoc />
        public virtual TUser Creator { get; set; }

        /// <inheritdoc />
        public virtual TUser LastModifier { get; set; }
    }
}
