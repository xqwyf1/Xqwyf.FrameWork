using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using JetBrains.Annotations;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 所有模块服务配置的上下文
    /// </summary>
    public class ServiceConfigurationContext
    {
        /// <summary>
        /// 模块的服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        public IDictionary<string, object> Items { get; }

        /// <summary>
        ///获取/设置可以在服务注册阶段存储并在模块之间共享的任意命名对象。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get => Items.GetOrDefault(key);
            set => Items[key] = value;
        }

        /// <summary>
        /// 创建一个  ServiceConfigurationContext对象
        /// </summary>
        /// <param name="services"></param>
        public ServiceConfigurationContext([NotNull] IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            Items = new Dictionary<string, object>();
        }
    }
}
