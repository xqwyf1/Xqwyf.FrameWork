using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Reflection;

namespace Xqwyf.Modularity
{
    internal static class XqModuleHelper
    {
        /// <summary>
        /// 从<paramref name="startupModuleType"/>开始，获取所有依赖的Module
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static List<Type> FindAllModuleTypes(Type startupModuleType)
        {
            var moduleTypes = new List<Type>();
            AddModuleAndDependenciesResursively(moduleTypes, startupModuleType);
            return moduleTypes;
        }

        /// <summary>
        /// 遍历依赖项，遍历中，每个Module只会加载一次
        /// </summary>
        /// <param name="moduleTypes">为引用值，填充后，在其他函数中使用</param>
        /// <param name="moduleType">初始的Module</param>
        private static void AddModuleAndDependenciesResursively(List<Type> moduleTypes, Type moduleType)
        {
            XqModule.CheckXqModuleType(moduleType);

            if (moduleTypes.Contains(moduleType))
            {
                return;
            }

            moduleTypes.Add(moduleType);

            foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
            {
                AddModuleAndDependenciesResursively(moduleTypes, dependedModuleType);
            }
        }

        /// <summary>
        /// 根据<see cref="IDependedTypesProvider"/>获取<paramref name="moduleType"/>所有依赖的Module
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            XqModule.CheckXqModuleType(moduleType);

            var dependencies = new List<Type>();

            var dependencyDescriptors = moduleType
                .GetCustomAttributes()
                .OfType<IDependedTypesProvider>();

            foreach (var descriptor in dependencyDescriptors)
            {
                foreach (var dependedModuleType in descriptor.GetDependedTypes())
                {
                    dependencies.AddIfNotContains(dependedModuleType);
                }
            }
            return dependencies;
        }

    }
}
