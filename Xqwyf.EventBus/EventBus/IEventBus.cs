using System;
using System.Threading;
using System.Threading.Tasks;

using Xqwyf.EventBus.Local;

namespace Xqwyf.EventBus
{
    /// <summary>
    /// 事件总线基础接口
    /// </summary>
    public interface IEventBus
    {

        #region 事件发布

        /// <summary>
        /// 事件总线发布（触发）一个事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventData">事件相关数据</param>
        /// <returns>处理异步的操作</returns>
        Task PublishAsync<TEvent>(TEvent eventData)
            where TEvent : class;

        /// <summary>
        ///  事件总线发布（触发）一个事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventData">事件相关数据</param>
        /// <returns>处理异步的操作</returns>
        Task PublishAsync(Type eventType, object eventData);

        #endregion


        #region 事件订阅
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
        /// 事件发生时， 调用<paramref name="handler"/>
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">事件处理句柄对象</param>
        IDisposable Subscribe(Type eventType, IEventHandler handler);

        /// <summary>
        /// 在事件总线中订阅一个事件;事件发生时，给定的<paramref name="factory"/>进行处理
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="factory">创建句柄的工厂</param>
        IDisposable Subscribe<TEvent>(IEventHandlerFactory factory)
            where TEvent : class;

        /// <summary>
        ///在事件总线中订阅一个事件;发生事件时，将通过<paramref name="factory"/>所产生的处理程序进行处理
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">事件处理程序工厂</param>
        IDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        #endregion


        #region 取消订阅

        /// <summary>
        /// 取消订阅<typeparamref name="TEvent"/>
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="action">所挂接的相关方法</param>
        void Unsubscribe<TEvent>(Func<TEvent, Task> action)
            where TEvent : class;

        /// <summary>
        /// 取消订阅<typeparamref name="TEvent"/>
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">所注册的相关事件处理程序</param>
        void Unsubscribe<TEvent>(ILocalEventHandler<TEvent> handler)
            where TEvent : class;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">所注册的相关事件处理程序</param>
        void Unsubscribe(Type eventType, IEventHandler handler);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="factory">所注册的相关事件处理程序工厂</param>
        void Unsubscribe<TEvent>(IEventHandlerFactory factory)
            where TEvent : class;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">所注册的相关事件处理程序工厂</param>
        void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        ///取消指定<typeparamref name="TEvent"/>的所有事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnsubscribeAll<TEvent>()
            where TEvent : class;

        /// <summary>
        ///取消指定<paramref name="eventType"/>的所有事件订阅
        /// </summary>
        /// <param name="eventType">Event type</param>
        void UnsubscribeAll(Type eventType);

        #endregion
    }
}