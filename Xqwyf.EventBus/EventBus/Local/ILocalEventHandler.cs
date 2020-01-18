using System.Threading.Tasks;

namespace Xqwyf.EventBus.Local
{
    /// <summary>
    /// 本地事件处理程序接口
    /// </summary>
    /// <typeparam name="TEvent">所要处理的事件</typeparam>
    public interface ILocalEventHandler<in TEvent> : IEventHandler
    {
        /// <summary>
        ///处理程序通过实现此方法来处理事件。
        /// </summary>
        /// <param name="eventData">事件数据</param>
        Task HandleEventAsync(TEvent eventData);
    }
}
