using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace  Xqwyf.Domain.Entities.Events
{
    /// <summary>
    /// 事件触发类，用于触发实体变更事件，包括创建前，创建后，修改前，修改后，删除前，删除后
    /// </summary>
    public interface IEntityChangeEventHelper
    {
        Task TriggerEventsAsync(EntityChangeReport changeReport);

        Task TriggerEntityCreatingEventAsync(object entity);
        Task TriggerEntityCreatedEventOnUowCompletedAsync(object entity);

        Task TriggerEntityUpdatingEventAsync(object entity);
        Task TriggerEntityUpdatedEventOnUowCompletedAsync(object entity);

        Task TriggerEntityDeletingEventAsync(object entity);
        Task TriggerEntityDeletedEventOnUowCompletedAsync(object entity);
    }
}
