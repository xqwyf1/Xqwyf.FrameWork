using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.EventBus
{
    public class EventHandlerDisposeWrapper : IEventHandlerDisposeWrapper
    {
        public IEventHandler EventHandler { get; }

        private readonly Action _disposeAction;

        /// <summary>
        /// 创建一个<see cref="EventHandlerDisposeWrapper"/>
        /// </summary>
        /// <param name="eventHandler">所包装的EventHandler</param>
        /// <param name="disposeAction">dispose函数</param>
        public EventHandlerDisposeWrapper(IEventHandler eventHandler, Action disposeAction = null)
        {
            _disposeAction = disposeAction;
            EventHandler = eventHandler;
        }

        public void Dispose()
        {
            _disposeAction?.Invoke();
        }
    }
}
