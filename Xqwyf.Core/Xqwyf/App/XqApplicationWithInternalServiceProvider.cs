using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace  Xqwyf.App
{
    /// <summary>
    /// IXqApplicationWithInternalServiceProvider实现
    /// </summary>
    internal class XqApplicationWithInternalServiceProvider : XqApplicationBase, IXqApplicationWithInternalServiceProvider
    {
        public IServiceScope ServiceScope { get; private set; }

        public XqApplicationWithInternalServiceProvider(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction
            ) : this(
            startupModuleType,
            new ServiceCollection(),
            optionsAction)
        {

        }

        private XqApplicationWithInternalServiceProvider(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction
            ) : base(
                startupModuleType,
                services,
                optionsAction)
        {
            Services.AddSingleton<IXqApplicationWithInternalServiceProvider>(this);
        }

        public void Initialize()
        {
            ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
            SetServiceProvider(ServiceScope.ServiceProvider);

            InitializeModules();
        }

        public override void Dispose()
        {
            base.Dispose();
            ServiceScope.Dispose();
        }
    }
}
