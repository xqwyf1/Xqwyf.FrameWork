using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xqwyf.Data;

namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 所有聚合的基础类，实现<see cref="IAggregateRoot"/>
    /// </summary>
    public abstract class AggregateRoot : 
        Entity, IAggregateRoot,IGeneratesDomainEvents,IHasExtraProperties, IHasConcurrencyStamp
    {
        #region 构造函数
        protected AggregateRoot()
        {
            ExtraProperties = new Dictionary<string, object>();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
        #endregion

        #region IHasExtraProperties实现

        /// <summary>
        /// 聚合根的扩展属性
        /// </summary>
        public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

        #endregion

        #region IHasConcurrencyStamp实现

        /// <summary>
        /// 并发戳
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }
        #endregion

        #region IGeneratesDomainEvents实现


        /// <summary>
        /// 本地事件存储
        /// </summary>
        private readonly ICollection<object> _localEvents = new Collection<object>();

        /// <summary>
        /// 添加本地事件
        /// </summary>
        /// <param name="eventData"></param>
        protected virtual void AddLocalEvent(object eventData)
        {
            _localEvents.Add(eventData);
        }

        /// <summary>
        /// 获取本地所有事件
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<object> GetLocalEvents()
        {
            return _localEvents;
        }

        /// <summary>
        /// 清除本地所有事件
        /// </summary>
        public virtual void ClearLocalEvents()
        {
            _localEvents.Clear();
        }

        #endregion
    }
}
