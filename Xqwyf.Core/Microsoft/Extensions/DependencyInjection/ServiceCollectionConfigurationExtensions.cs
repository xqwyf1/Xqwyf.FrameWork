using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace  Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionConfigurationExtensions
    {
        /// <summary>
        /// 用<paramref name="configuration"/>替换<paramref name="services"/>中类型为<see cref="IConfiguration"/>的服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Replace(ServiceDescriptor.Singleton<IConfiguration>(configuration));
        }

        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();
            if (hostBuilderContext?.Configuration != null)
            {
                return hostBuilderContext.Configuration as IConfigurationRoot;
            }

            return services.GetSingletonInstance<IConfiguration>();
        }
    }
}
