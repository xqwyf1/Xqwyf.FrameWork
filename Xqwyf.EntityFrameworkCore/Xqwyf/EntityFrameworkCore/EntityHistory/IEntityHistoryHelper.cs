using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xqwyf.Auditing;

namespace  Xqwyf.EntityFrameworkCore.EntityHistory
{
    /// <summary>
    /// 实体变更情况记录帮助类，创建、修改实体的变更列表
    /// </summary>
    public interface IEntityHistoryHelper
    {
        /// <summary>
        /// 在保存前。根据<paramref name="entityEntries"/>,创建实体的变更情况记录列表
        /// </summary>
        /// <param name="entityEntries"></param>
        /// <returns></returns>
        List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries);

        /// <summary>
        /// 在保存后，修改ChangeList,
        /// </summary>
        /// <param name="entityChanges"></param>
        void UpdateChangeList(List<EntityChangeInfo> entityChanges);
    }
}
