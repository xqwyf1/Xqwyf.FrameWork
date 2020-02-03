using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Data
{
    /// <summary>
    /// 逻辑删除接口，标记一个实体已经被删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 用于标记一个实体已经被删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
