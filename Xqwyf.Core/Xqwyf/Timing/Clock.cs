using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Xqwyf.Core.Timing
{
    /// <summary>
    /// 时钟实现
    /// </summary>
    public class Clock : IClock
    {
        protected XqClockOptions Options { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">加载的选项</param>
        public Clock(IOptions<XqClockOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 获取当前时间，如果选项是世界标准时间（UTC），则返回世界标准时间，否则返回当前机器时间
        /// </summary>
        public virtual DateTime Now => Options.Kind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;

        /// <summary>
        /// 获取所有时间选项
        /// </summary>
        public virtual DateTimeKind Kind => Options.Kind;


        public virtual bool SupportsMultipleTimezone => Options.Kind == DateTimeKind.Utc;

        public DateTime Normalize(DateTime dateTime)
        {
            if (Kind == DateTimeKind.Unspecified || Kind == dateTime.Kind)
            {
                return dateTime;
            }

            if (Kind == DateTimeKind.Local && dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            if (Kind == DateTimeKind.Utc && dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return DateTime.SpecifyKind(dateTime, Kind);
        }
    }
}
