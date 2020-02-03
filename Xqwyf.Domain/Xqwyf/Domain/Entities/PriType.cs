using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain.Entities
{

    /// <summary>
    /// 主键的数据类型
    /// </summary>
    public enum PriType : byte
    {
        Int = 0,

        Str = 1,

        Deleted = 2,

        Guids=4,
    }
}
