using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Domain.Entities
{
    /// <summary>
    /// 并发戳接口
    /// </summary>
    public interface IHasConcurrencyStamp
    {
        /// <summary>
        /// 并发戳
        /// </summary>
        string ConcurrencyStamp { get; set; }
    }
}
