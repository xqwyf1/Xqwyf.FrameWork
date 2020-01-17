using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 所有聚合的基础类
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        #region 本地事件

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
        /// 获取本地事件
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<object> GetLocalEvents()
        {
            return _localEvents;
        }

        /// <summary>
        /// 清除本地事件
        /// </summary>
        public virtual void ClearLocalEvents()
        {
            _localEvents.Clear();
        }

        #endregion
    }
}
