using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.DependencyInjection;
using Xqwyf.Internal;
using Xqwyf.Modularity;
using Xqwyf.Modularity.PlugIns;

namespace  Xqwyf
{
    /// <summary>
    /// 应用基础类
    /// </summary>
    public abstract class XqApplicationBase : IXqApplication
    {
        #region IXqApplication实现

        [NotNull]
        public Type StartupModuleType { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IServiceCollection Services { get; }

        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        #endregion

        #region IModuleContainer 实现

        public IReadOnlyList<IXqModuleDescriptor> Modules { get; }


        #endregion

        #region IDisposable实现

        public virtual void Dispose()
        {
            //TODO: Shutdown if not done before?
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 根据<paramref name="startupModuleType"/>,以及<paramref name="services"/>和<paramref name="optionsAction"/>,创建一个XqApplication
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

            ///增加应用服务
            services.AddSingleton<IXqApplication>(this);

            ///增加<see cref="IModuleContainer"/>服务
            services.AddSingleton<IModuleContainer>(this);

            services.AddCoreServices();
            services.AddCoreXqServices(this, options);

            Modules = LoadModules(services, options);
        }


        #endregion

       
        /// <summary>
        /// 根据提供的<paramref name="serviceProvider"/>,设置当前应用的<see cref="IServiceProvider"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
        }

        /// <summary>
        /// 进行Module的初始化
        /// </summary>
        protected virtual void InitializeModules()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        /// <summary>
        /// 从<paramref name="services"/>中，通过<paramref name="options"/>配置，加载所有Module
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns>返回所有的Modules</returns>
        private IReadOnlyList<IXqModuleDescriptor> LoadModules(IServiceCollection services, XqApplicationCreationOptions options)
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
