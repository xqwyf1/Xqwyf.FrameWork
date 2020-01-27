using JetBrains.Annotations;

using Xqwyf.App;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// IModuleManager接口，管理所有<see cref="XqModule"/>的Initialize和Shutdown
    /// </summary>
    public interface IModuleManager
    {
        void InitializeModules([NotNull] ApplicationInitializationContext context);

        void ShutdownModules([NotNull] ApplicationShutdownContext context);
    }
}
