using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Core.Timing
{
    /// <summary>
    /// 时间选项
    /// </summary>
    public class XqClockOptions
    {
        /// <summary>
        /// 默认: <see cref="DateTimeKind.Unspecified"/>
        /// </summary>
        public DateTimeKind Kind { get; set; }

        public XqClockOptions()
        {
            Kind = DateTimeKind.Unspecified;
        }
    }
}
