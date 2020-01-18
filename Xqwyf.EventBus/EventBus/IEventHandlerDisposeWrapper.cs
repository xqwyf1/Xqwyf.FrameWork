using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.EventBus
{
    /// <summary>
    /// 事件处理程序包装接口
    /// </summary>
    public interface IEventHandlerDisposeWrapper : IDisposable
    {
        /// <summary>
        /// 事件处理程序
        /// </summary>
        IEventHandler EventHandler { get; }
    }
}
