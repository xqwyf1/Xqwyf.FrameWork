using System;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 所有模块的接口
    /// </summary>
    public interface IXqModule
    {
        /// <summary>
        /// 用于在模块加载时，进行模块中服务配置
        /// </summary>
        /// <param name="context">服务加载的上下文</param>
        void ConfigureServices(ServiceConfigurationContext context);
    }
}
