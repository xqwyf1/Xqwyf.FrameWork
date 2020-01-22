using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.Modularity;

namespace  Xqwyf
{
    /// <summary>
    /// 通过本工厂，创建应用，启动应用
    /// </summary>
    public static class XqApplicationFactory
    {
        public static IXqApplicationWithInternalServiceProvider Create<TStartupModule>(
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IXqModule
        {
            return Create(typeof(TStartupModule), optionsAction);
        }

        public static IXqApplicationWithInternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
        {
            return new XqApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
        }

        public static IXqApplicationWithExternalServiceProvider Create<TStartupModule>(
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IXqModule
        {
            return Create(typeof(TStartupModule), services, optionsAction);
        }

        public static IXqApplicationWithExternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
        {
            return new XqApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
        }
    }
}
