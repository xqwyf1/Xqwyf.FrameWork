using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.Auditing;

namespace  Xqwyf.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObjectObject{TUser}"/> for aggregate roots.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    [Serializable]
    public abstract class AuditedAggregateRootWithUser<TUser> : AuditedAggregateRoot, IAuditedObject<TUser>
        where TUser : IEntity
    {
        /// <inheritdoc />
        public virtual TUser Creator { get; set; }

        /// <inheritdoc />
        public virtual TUser LastModifier { get; set; }
    }
}
