using System;
using System.Threading;
using System.Threading.Tasks;

namespace Xqwyf.EventBus
{
    public interface IEventBus
    {
        /// <summary>
        /// 发布（触发）一个事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventData">事件相关数据</param>
        /// <returns>处理异步的操作</returns>
        Task PublishAsync<TEvent>(TEvent eventData)
            where TEvent : class;

        /// <summary>
        ///  发布（触发）一个事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventData">事件相关数据</param>
        /// <returns>处理异步的操作</returns>
        Task PublishAsync(Type eventType, object eventData);

        /// <summary>
        /// 在事件总线中订阅一个事件;
        ///订阅事件发生时，将调用给定的action
        /// </summary>
        /// <param name="action">调用的Action</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        IDisposable Subscribe<TEvent>(Func<TEvent, Task> action)
            where TEvent : class;

        /// <summary>
        /// 在事件总线中订阅一个事件;
        ///事件发生时， 创建<see cref="THandler"/>的新实例 ；
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <typeparam name="THandler">事件处理句柄的类型</typeparam>
        IDisposable Subscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler, new();

        /// <summary>
        /// 在事件总线中订阅一个事件;
        /// Same (given) instance of the handler is used for all event occurrences.
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">事件处理句柄对象</param>
        IDisposable Subscribe(Type eventType, IEventHandler handler);

        /// <summary>
        /// 在事件总线中订阅一个事件;
        /// Given factory is used to create/release handlers
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="factory">A factory to create/release handlers</param>
        IDisposable Subscribe<TEvent>(IEventHandlerFactory factory)
            where TEvent : class;

        /// <summary>
        ///在事件总线中订阅一个事件;
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="factory">A factory to create/release handlers</param>
        IDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="action"></param>
        void Unsubscribe<TEvent>(Func<TEvent, Task> action)
            where TEvent : class;

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="handler">Handler object that is registered before</param>
        void Unsubscribe<TEvent>(ILocalEventHandler<TEvent> handler)
            where TEvent : class;

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="handler">Handler object that is registered before</param>
        void Unsubscribe(Type eventType, IEventHandler handler);

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="factory">Factory object that is registered before</param>
        void Unsubscribe<TEvent>(IEventHandlerFactory factory)
            where TEvent : class;

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="factory">Factory object that is registered before</param>
        void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        void UnsubscribeAll<TEvent>()
            where TEvent : class;

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// </summary>
        /// <param name="eventType">Event type</param>
        void UnsubscribeAll(Type eventType);
    }
}
}
