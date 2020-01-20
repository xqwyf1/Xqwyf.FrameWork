using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// 准备进行Config服务的接口
    /// </summary>
    public interface IPreConfigureServices
    {
        /// <summary>
        /// 配置服务前的操作
        /// </summary>
        /// <param name="context"></param>
        void PreConfigureServices(ServiceConfigurationContext context);
    }
}
