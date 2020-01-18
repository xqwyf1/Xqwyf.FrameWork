using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xqwyf.EventBus.Local;

namespace Xqwyf.EventBus
{
    /// <summary>
    /// 事件总线基础类
    /// </summary>
    public  abstract class EventBusBase : IEventBus
    {

        #region 订阅

        public virtual IDisposable Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
        {
            return Subscribe(typeof(TEvent), new ActionEventHandler<TEvent>(action));
        }

        public virtual IDisposable Subscribe<TEvent, THandler>()
          where TEvent : class
          where THandler : IEventHandler, new()
        {
            return Subscribe(typeof(TEvent), new TransientEventHandlerFactory<THandler>());
        }

        public virtual IDisposable Subscribe(Type eventType, IEventHandler handler)
        {
            return Subscribe(eventType, new SingleInstanceHandlerFactory(handler));
        }

        public abstract IDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        public virtual IDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            return Subscribe(typeof(TEvent), factory);
        }


        #endregion

        #region 取消订阅

        public abstract void Unsubscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class;

        /// <inheritdoc/>
        public virtual void Unsubscribe<TEvent>(ILocalEventHandler<TEvent> handler) where TEvent : class
        {
            Unsubscribe(typeof(TEvent), handler);
        }

        public abstract void Unsubscribe(Type eventType, IEventHandler handler);

        /// <inheritdoc/>
        public virtual void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            Unsubscribe(typeof(TEvent), factory);
        }

        public abstract void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        /// <inheritdoc/>
        public virtual void UnsubscribeAll<TEvent>() where TEvent : class
        {
            UnsubscribeAll(typeof(TEvent));
        }

        /// <inheritdoc/>
        public abstract void UnsubscribeAll(Type eventType);

        #endregion

        #region 发布事件

        public virtual Task PublishAsync<TEvent>(TEvent eventData) where TEvent : class
        {
            return PublishAsync(typeof(TEvent), eventData);
        }

        /// <inheritdoc/>
        public abstract Task PublishAsync(Type eventType, object eventData);


        #endregion
    }
}
