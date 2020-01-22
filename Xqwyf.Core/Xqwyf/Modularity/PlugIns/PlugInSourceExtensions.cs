using System;
using System.Linq;
using JetBrains.Annotations;
using Xqwyf.Modularity;

namespace  Xqwyf.Modularity.PlugIns
{
    public static class PlugInSourceExtensions
    {
        /// <summary>
        /// 获取<paramref name="plugInSource"/>的所有依赖Module
        /// </summary>
        /// <param name="plugInSource"></param>
        /// <returns></returns>
        [NotNull]
        public static Type[] GetModulesWithAllDependencies([NotNull] this IPlugInSource plugInSource)
        {
            Check.NotNull(plugInSource, nameof(plugInSource));

            return plugInSource
                .GetModules()
                .SelectMany(XqModuleHelper.FindAllModuleTypes)
                .Distinct()
                .ToArray();
        }
    }
}
