using System;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 所有模块的接口
    /// </summary>
    public interface IXqModule
    {
        /// <summary>
        /// 模块中所配置的服务
        /// </summary>
        /// <param name="context"></param>
        void ConfigureServices(ServiceConfigurationContext context);
    }
}
