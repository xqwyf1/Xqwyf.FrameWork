using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Modularity
{
    /// <summary>
    ///预配置服务的接口
    /// </summary>
    public interface IPreConfigureServices
    {
        /// <summary>
        /// 预配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        void PreConfigureServices(ServiceConfigurationContext context);
    }
}
