using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// 通过本接口获取每个Module的依赖Module
    /// </summary>
    public interface IDependedTypesProvider
    {
        /// <summary>
        /// 获取依赖的类型
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Type[] GetDependedTypes();
    }
}
