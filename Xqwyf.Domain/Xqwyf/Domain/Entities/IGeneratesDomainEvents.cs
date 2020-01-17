using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 领域事件的处理接口，只用于聚合根
    /// </summary>
    public interface IGeneratesDomainEvents
    {
        /// <summary>
        /// 获取所有领域事件
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetLocalEvents();


        /// <summary>
        /// 清除所有领域事件
        /// </summary>

        void ClearLocalEvents();

    }
}
