using System;
using JetBrains.Annotations;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// 定义一个Module的相关依赖
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        /// <summary>
        /// 依赖的类型
        /// </summary>
        [NotNull]
        public Type[] DependedTypes { get; }

        public DependsOnAttribute(params Type[] dependedTypes)
        {
            DependedTypes = dependedTypes ?? new Type[0];
        }

        public virtual Type[] GetDependedTypes()
        {
            return DependedTypes;
        }
    }
}
