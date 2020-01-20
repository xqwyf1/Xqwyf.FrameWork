using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.DependencyInjection;

using Xqwyf.Modularity;

namespace  Xqwyf
{
    /// <summary>
    /// 应用基础类
    /// </summary>
    public abstract class XqApplicationBase : IXqApplication
    {
        [NotNull]
        public Type StartupModuleType { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IServiceCollection Services { get; }

        public IReadOnlyList<IXqModuleDescriptor> Modules { get; }

        /// <summary>
        /// 创建一个AbpApplication
        /// </summary>
        /// <param name="startupModuleType">启动模块的类型</param>
        /// <param name="services">该应用所需的服务</param>
        /// <param name="optionsAction"></param>
        internal XqApplicationBase(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction)
        {
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(services, nameof(services));

            StartupModuleType = startupModuleType;
            Services = services;

            services.TryAddObjectAccessor<IServiceProvider>();

            var options = new XqApplicationCreationOptions(services);
            optionsAction?.Invoke(options);

            services.AddSingleton<IXqApplication>(this);
            services.AddSingleton<IModuleContainer>(this);

            services.AddCoreServices();
            services.AddCoreAbpServices(this, options);

            Modules = LoadModules(services, options);
        }

        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        public virtual void Dispose()
        {
            //TODO: Shutdown if not done before?
        }

        protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
        }

        protected virtual void InitializeModules()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        private IReadOnlyList<IXqModuleDescriptor> LoadModules(IServiceCollection services, AbpApplicationCreationOptions options)
        {
            return services
                .GetSingletonInstance<IModuleLoader>()
                .LoadModules(
                    services,
                    StartupModuleType,
                    options.PlugInSources
                );
        }
    }
}
