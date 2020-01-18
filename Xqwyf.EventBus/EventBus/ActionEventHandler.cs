using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xqwyf.EventBus.Local;

namespace Xqwyf.EventBus
{
    /// <summary>
    /// 这个事件处理程序是一个适配器，将一个 action转换为 <see cref="ILocalEventHandler{TEvent}"/> 
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    public class ActionEventHandler<TEvent> :
        ILocalEventHandler<TEvent>
    {
        /// <summary>
        ///用于处理事件的程序
        /// </summary>
        public Func<TEvent, Task> Action { get; }

        /// <summary>
        ///创建一个<see cref="ActionEventHandler{TEvent}"/>.
        /// </summary>
        /// <param name="handler">用于处理事件的程序</param>
        public ActionEventHandler(Func<TEvent, Task> handler)
        {
            Action = handler;
        }

        /// <summary>
        ///处理事件
        /// </summary>
        /// <param name="eventData">事件数据</param>
        public async Task HandleEventAsync(TEvent eventData)
        {
            await Action(eventData).ConfigureAwait(false);
        }
    }
}
