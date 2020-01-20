using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;


namespace Xqwyf.Modularity
{
    /// <summary>
    /// 模块描述，描述每个模块的基础信息
    /// </summary>
    public interface IXqModuleDescriptor
    {
        /// <summary>
        /// 模块的类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 模块所在的程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 创建的模块实例
        /// </summary>
        IAbpModule Instance { get; }

        /// <summary>
        /// 模块是否是外部加载
        /// </summary>
        bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 每个模块所依赖的模块
        /// </summary>
        IReadOnlyList<IXqModuleDescriptor> Dependencies { get; }
    }
}
