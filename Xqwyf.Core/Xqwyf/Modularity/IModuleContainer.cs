using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 模块容器接口，管理已经加载的模块
    /// </summary>
    public interface IModuleContainer
    {
        /// <summary>
        /// 记录所有已经加载的模块
        /// </summary>
        [NotNull]
        IReadOnlyList<IXqModuleDescriptor> Modules { get; }
    }
}
