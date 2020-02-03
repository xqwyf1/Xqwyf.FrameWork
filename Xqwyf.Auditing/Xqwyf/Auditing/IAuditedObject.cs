using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace  Xqwyf.Auditing
{
    /// <summary>
    /// 包括基础的审计属性，有创建者，创建时间，修改人，修改时间
    /// </summary>
    public interface IAuditedObject 
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        Guid? CreatorId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者ID
        /// </summary>
        Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// <see cref="IAuditedObject"/>扩展属性，包括Creator和LastModifier的导航属性
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IAuditedObject<TUser> : IAuditedObject
    {
        /// <summary>
        /// 关于创建者的引用
        /// </summary>
        [CanBeNull]
        TUser Creator { get; set; }

        /// <summary>
        /// 关于最后修改者的引用
        /// </summary>
        TUser LastModifier { get; set; }
    }
}
