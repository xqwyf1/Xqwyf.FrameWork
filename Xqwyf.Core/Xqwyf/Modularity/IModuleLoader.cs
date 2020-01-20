using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

using JetBrains.Annotations;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// 模块加载接口，应用通过本对象加载所有涉及到的模块
    /// </summary>
    public interface IModuleLoader
    {
        [NotNull]
        IXqModuleDescriptor[] LoadModules(
            [NotNull] IServiceCollection services,
            [NotNull] Type startupModuleType,
            [NotNull] PlugInSourceList plugInSources
        );
    }
}
