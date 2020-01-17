using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.EventBus.Local
{
    /// <summary>
    ///定义一个本地事件的接口
    /// </summary>
    public interface ILocalEventBus : IEventBus
    {
        /// <summary>
        /// 订阅事件
        /// Same (given) instance of the handler is used for all event occurrences.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">处理事件的句柄</param>
        IDisposable Subscribe<TEvent>(ILocalEventHandler<TEvent> handler)
            where TEvent : class;
    }
}
