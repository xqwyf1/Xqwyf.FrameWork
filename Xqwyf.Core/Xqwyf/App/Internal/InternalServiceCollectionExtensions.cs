using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xqwyf.Modularity;
using Xqwyf.Reflection;
using Xqwyf.App;

namespace  Xqwyf.App.Internal
{
    internal static class InternalServiceCollectionExtensions
    {
        /// <summary>
        /// 添加核心服务
        /// </summary>
        /// <param name="services"></param>
        internal static void AddCoreServices(this IServiceCollection services)
        {
            ///增加Option服务
            services.AddOptions();

            ///增加Log服务
            services.AddLogging();

            ///增加本地化服务
            services.AddLocalization();
        }

        /// <summary>
        /// 在当前应用中添加本框架中的核心服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="xqApplication">当前应用</param>
        /// <param name="applicationCreationOptions">应用创建选项</param>
        internal static void AddCoreXqServices(this IServiceCollection services,
            IXqApplication xqApplication,
            XqApplicationCreationOptions applicationCreationOptions)
        {
            var moduleLoader = new ModuleLoader();
            var assemblyFinder = new AssemblyFinder(xqApplication);
            var typeFinder = new TypeFinder(assemblyFinder);

            if (!services.IsAdded<IConfiguration>())
            {
                services.ReplaceConfiguration(
                    ConfigurationHelper.BuildConfiguration(
                        applicationCreationOptions.Configuration
                    )
                );
            }

            services.TryAddSingleton<IModuleLoader>(moduleLoader);

            services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);

            services.TryAddSingleton<ITypeFinder>(typeFinder);

            services.AddAssemblyOf<IXqApplication>();

            services.Configure<XqModuleLifecycleOptions>(options =>
            {
                options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
            });
        }
    }
}
