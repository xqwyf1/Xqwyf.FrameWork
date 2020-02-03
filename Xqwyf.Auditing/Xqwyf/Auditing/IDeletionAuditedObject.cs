using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.Data;

namespace  Xqwyf.Auditing
{
    /// <summary>
    /// This interface can be implemented to store deletion information (who delete and when deleted).
    /// </summary>
    public interface IDeletionAuditedObject:ISoftDelete
    {
        /// <summary>
        ///删除者的ID
        /// </summary>
        Guid? DeleterId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// Extends <see cref="IDeletionAuditedObject"/> to add user navigation propery.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IDeletionAuditedObject<TUser> : IDeletionAuditedObject
    {
        /// <summary>
        /// 删除者的的引用
        /// </summary>
        TUser Deleter { get; set; }
    }
}
