using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.Modularity.PlugIns;

namespace  Xqwyf
{
    /// <summary>
    /// 应用创建时的选项
    /// </summary>
    public class XqApplicationCreationOptions
    {
        [NotNull]
        public IServiceCollection Services { get; }

        [NotNull]
        public PlugInSourceList PlugInSources { get; }
        /// <summary>
        /// 应用环境配置选项
        /// </summary>
        [NotNull]
        public XqConfigurationBuilderOptions Configuration { get; }


        /// <summary>
        /// 创建一个<see cref="XqApplicationCreationOptions"/>对象,同时设置选项中的<paramref name="services"/>
        /// </summary>
        /// <param name="services">创建时使用的服务</param>
        public XqApplicationCreationOptions([NotNull] IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
          
            Configuration = new XqConfigurationBuilderOptions();
        }
    }
}
