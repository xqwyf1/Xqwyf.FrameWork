using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Xqwyf.EventBus
{
    /// <summary>
    /// 单例事件处理工厂，总是获取一个处理程序的单例
    /// </summary>
    public class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 处理程序实例
        /// </summary>
        public IEventHandler HandlerInstance { get; }

        /// <summary>
        /// 创建一个<see cref="SingleInstanceHandlerFactory"/>对象
        /// </summary>
        /// <param name="handler">该工厂的事件处理实例</param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        /// <summary>
        /// 获取事件处理程序的Dispose包装对象
        /// </summary>
        /// <returns></returns>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            return new EventHandlerDisposeWrapper(HandlerInstance);
        }

        /// <summary>
        /// 判断该事件处理程序工厂是否已经创建
        /// </summary>
        /// <param name="handlerFactories"></param>
        /// <returns></returns>
        public bool IsInFactories(List<IEventHandlerFactory> handlerFactories)
        {
            return handlerFactories
                .OfType<SingleInstanceHandlerFactory>()
                .Any(f => f.HandlerInstance == HandlerInstance);
        }
    }
}
