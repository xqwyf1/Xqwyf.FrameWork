using System;
using JetBrains.Annotations;

namespace  Xqwyf
{
    /// <summary>
    /// 外部服务应用提供者,需要提供<see cref="IServiceProvider"/>
    /// </summary>
    public interface IXqApplicationWithExternalServiceProvider : IXqApplication
    {
        /// <summary>
        /// 初始化应用，需要提供<paramref name="serviceProvider"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        void Initialize([NotNull] IServiceProvider serviceProvider);
    }
}
