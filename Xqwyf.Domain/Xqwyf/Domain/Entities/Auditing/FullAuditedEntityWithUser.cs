using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.Auditing;

namespace  Xqwyf.Domain.Entities.Auditing
{
    public abstract class FullAuditedEntityWithUser<TUser> : FullAuditedEntity, IFullAuditedObject<TUser>
       where TUser : IEntity
    {
        /// <inheritdoc />
        public virtual TUser Deleter { get; set; }

        /// <inheritdoc />
        public TUser Creator { get; set; }

        /// <inheritdoc />
        public TUser LastModifier { get; set; }
    }

}
