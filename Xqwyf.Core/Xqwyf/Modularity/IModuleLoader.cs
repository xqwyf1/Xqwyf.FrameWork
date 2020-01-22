using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

using JetBrains.Annotations;

using Xqwyf.Modularity.PlugIns;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// 模块加载接口，应用通过本接口加载所有涉及到的模块
    /// </summary>
    public interface IModuleLoader
    {
        /// <summary>
        /// 模块加载操作，并返回所有的Modules
        /// </summary>
        /// <param name="services">相关的Service</param>
        /// <param name="startupModuleType">启动Module的类型</param>
        /// <param name="plugInSources">插件来源</param>
        /// <returns></returns>
        [NotNull]
        IXqModuleDescriptor[] LoadModules(
            [NotNull] IServiceCollection services,
            [NotNull] Type startupModuleType,
            [NotNull] PlugInSourceList plugInSources
        );
    }
}
