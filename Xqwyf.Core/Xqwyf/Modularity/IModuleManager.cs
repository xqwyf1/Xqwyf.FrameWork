using JetBrains.Annotations;

using Xqwyf.App;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// IModuleManager接口，管理所有<see cref="XqModule"/>的Initialize和Shutdown
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 通过<paramref name="context"/>进行IModuleManager中所有Module的初始化
        /// </summary>
        void InitializeModules([NotNull] ApplicationInitializationContext context);

        /// <summary>
        /// 通过<paramref name="context"/>进行IModuleManager中所有Module的注销
        /// </summary>
        /// <param name="context"></param>
        void ShutdownModules([NotNull] ApplicationShutdownContext context);
    }
}
