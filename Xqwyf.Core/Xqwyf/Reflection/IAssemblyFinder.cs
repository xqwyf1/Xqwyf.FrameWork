using System.Collections.Generic;
using System.Reflection;

namespace  Xqwyf.Reflection
{
    /// <summary>
    /// 用于获取应用中所有程序集
    /// </summary>
    public interface IAssemblyFinder
    {
        IReadOnlyList<Assembly> Assemblies { get; }
    }
}
