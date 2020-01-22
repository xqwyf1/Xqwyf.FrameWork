using JetBrains.Annotations;
namespace  Xqwyf.Modularity
{
    /// <summary>
    /// IModuleManager接口，管理Module的Initialize和Shutdown
    /// </summary>
    public interface IModuleManager
    {
        void InitializeModules([NotNull] ApplicationInitializationContext context);

        void ShutdownModules([NotNull] ApplicationShutdownContext context);
    }
}
