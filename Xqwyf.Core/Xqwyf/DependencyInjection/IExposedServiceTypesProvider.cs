using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.DependencyInjection
{
    /// <summary>
    /// 暴露服务类型提供者
    /// </summary>
    public interface IExposedServiceTypesProvider
    {
        /// <summary>
        /// 获取<paramref name="targetType"/>最终暴露的服务列表
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        Type[] GetExposedServiceTypes(Type targetType);
    }
}
