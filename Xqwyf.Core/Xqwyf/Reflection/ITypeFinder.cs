using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Reflection
{
    /// <summary>
    /// 获取应用所有的类型.
    /// </summary>
    public interface ITypeFinder
    {
        IReadOnlyList<Type> Types { get; }
    }
}
