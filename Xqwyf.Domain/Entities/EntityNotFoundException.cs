using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain.Entities
{
   public  class EntityNotFoundException:Xqwyf.Core.XqExecption
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        ///实体主键
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// 创建一个 <see cref="EntityNotFoundException"/> 对象.
        /// </summary>
        public EntityNotFoundException()
        {

        }

        /// <summary>
        /// 创建一个 <see cref="EntityNotFoundException"/> 对象.
        /// </summary>
        /// <param name="entityType">实体类型</param>
        public EntityNotFoundException(Type entityType)
            : this(entityType, null, null)
        {

        }

        /// <summary>
        /// 创建一个 <see cref="EntityNotFoundException"/> 对象.
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="id">实体主键</param>
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }


        /// <summary>
        /// 创建一个 <see cref="EntityNotFoundException"/> 对象.
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="id">实体主键</param>
        /// <param name="innerException">内联错误</param>
        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"没有找到实体类型为: {entityType.FullName}并且主键为： {id}的实体", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        /// <summary>
        ///创建一个 <see cref="EntityNotFoundException"/> 对象.
        /// </summary>
        /// <param name="message">异常消息</param>
        public EntityNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 创建一个 <see cref="EntityNotFoundException"/> 对象.
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内联错误</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

    }
}
