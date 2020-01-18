using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.EventBus
{
    /// <summary>
    ///定义一个用于产生事件处理程序的工厂
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取一个事件处理程序
        /// </summary>
        /// <returns>The event handler</returns>
        IEventHandlerDisposeWrapper GetHandler();

        /// <summary>
        /// 判断当前事件处理工厂是否已经创建
        /// </summary>
        /// <param name="handlerFactories"></param>
        /// <returns></returns>
        bool IsInFactories(List<IEventHandlerFactory> handlerFactories);
    }
}