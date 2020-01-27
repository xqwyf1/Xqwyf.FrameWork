using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 记录每个Module的信息描述
    /// </summary>
    public class XqModuleDescriptor : IXqModuleDescriptor
    {
        public Type Type { get; }

        public Assembly Assembly { get; }

        public IXqModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public IReadOnlyList<IXqModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
        private readonly List<IXqModuleDescriptor> _dependencies;

        /// <summary>
        ///根据<paramref name="type"/>,<paramref name="instance"/>, 创建一个<see cref="XqModuleDescriptor"/>对象
        /// </summary>
        /// <param name="type">模块的类型</param>
        /// <param name="instance">模块的实例</param>
        /// <param name="isLoadedAsPlugIn">是否为外部加载</param>
        public XqModuleDescriptor(
            [NotNull] Type type,
            [NotNull] IXqModule instance,
            bool isLoadedAsPlugIn)
        {
            XqCheck.NotNull(type, nameof(type));
            XqCheck.NotNull(instance, nameof(instance));

            ///type与instance的type不一样
            if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
            }

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;

            _dependencies = new List<IXqModuleDescriptor>();
        }

        /// <summary>
        /// 添加该模块的依赖模块
        /// </summary>
        /// <param name="descriptor">模块的描述</param>
        public void AddDependency(IXqModuleDescriptor descriptor)
        {
            _dependencies.AddIfNotContains(descriptor);
        }

        public override string ToString()
        {
            return $"[AbpModuleDescriptor {Type.FullName}]";
        }
    }
}
