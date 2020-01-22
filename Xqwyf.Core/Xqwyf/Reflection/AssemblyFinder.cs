using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xqwyf.Modularity;

namespace  Xqwyf.Reflection
{

    /// <summary>
    /// 获取<see cref="IModuleContainer"/>中的程序集
    /// </summary>
    public class AssemblyFinder : IAssemblyFinder
    {
        private readonly IModuleContainer _moduleContainer;

        private readonly Lazy<IReadOnlyList<Assembly>> _assemblies;

        /// <summary>
        /// 创建一个<see cref="AssemblyFinder"/>,获取<paramref name="moduleContainer"/>中的所有程序集
        /// </summary>
        /// <param name="moduleContainer"></param>
        public AssemblyFinder(IModuleContainer moduleContainer)
        {
            _moduleContainer = moduleContainer;

            _assemblies = new Lazy<IReadOnlyList<Assembly>>(FindAll, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IReadOnlyList<Assembly> Assemblies => _assemblies.Value;

        public IReadOnlyList<Assembly> FindAll()
        {
            var assemblies = new List<Assembly>();

            foreach (var module in _moduleContainer.Modules)
            {
                assemblies.Add(module.Type.Assembly);
            }

            return assemblies.Distinct().ToImmutableList();
        }
    }
}
