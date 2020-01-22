using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace  Xqwyf.Modularity.PlugIns
{
    /// <summary>
    /// 获取List中所有<see cref="IPlugInSource"/>的依赖Module
    /// </summary>
    public class PlugInSourceList : List<IPlugInSource>
    {
        /// <summary>
        /// 获取List中所有<see cref="IPlugInSource"/>的依赖Module
        /// </summary>
        /// <returns></returns>
        [NotNull]
        
        internal Type[] GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToArray();
        }
    }
}
