using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Timing
{
    /// <summary>
    /// 时间接口
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// 获取时间类型
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// 是否支持多个时区
        /// </summary>
        bool SupportsMultipleTimezone { get; }

        /// <summary>
        /// 规范给定的<see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">要被规范的<see cref="DateTime"/></param>
        /// <returns>被规范的DateTime</returns>
        DateTime Normalize(DateTime dateTime);
    }
}
