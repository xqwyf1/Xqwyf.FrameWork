using System;

namespace Xqwyf.Application.Dtos
{
    /// <summary>
    /// 本接口用于标准化方式返回列表数量
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        long TotalCount { get; set; }
    }
}
