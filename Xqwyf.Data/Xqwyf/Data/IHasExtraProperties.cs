using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Data
{
    /// <summary>
    /// 定义一个具有扩展属性接口
    /// </summary>
    public interface IHasExtraProperties
    {
        Dictionary<string, object> ExtraProperties { get; }
    }
}
