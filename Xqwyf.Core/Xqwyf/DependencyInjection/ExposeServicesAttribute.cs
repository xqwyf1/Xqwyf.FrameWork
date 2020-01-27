using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using System.Linq;

namespace  Xqwyf.DependencyInjection
{
    /// <summary>
    /// 定义相关服务暴露为哪些服务
    /// </summary>
    public class ExposeServicesAttribute : Attribute, IExposedServiceTypesProvider
    {
        /// <summary>
        /// 暴露成哪些<see cref="Type"/>的服务
        /// </summary>
        public Type[] ServiceTypes { get; }

        /// <summary>
        /// 是否包括默认服务接口
        /// </summary>
        public bool IncludeDefaults { get; set; }

        /// <summary>
        /// 暴露时，是否包括自身服务
        /// </summary>
        public bool IncludeSelf { get; set; }

        public ExposeServicesAttribute(params Type[] serviceTypes)
        {
            ServiceTypes = serviceTypes ?? new Type[0];
        }

        public Type[] GetExposedServiceTypes(Type targetType)
        {
            var serviceList = ServiceTypes.ToList();

            if (IncludeDefaults)
            {
                foreach (var type in GetDefaultServices(targetType))
                {
                    serviceList.AddIfNotContains(type);
                }
            }

            if (IncludeSelf)
            {
                serviceList.AddIfNotContains(targetType);
            }

            return serviceList.ToArray();
        }

        /// <summary>
        /// 获取<paramref name="type"/>的默认接口服务，默认接口服务名称应该是指去掉接口字母"I"之后的服务名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<Type> GetDefaultServices(Type type)
        {
            var serviceTypes = new List<Type>();

            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                var interfaceName = interfaceType.Name;

                if (interfaceName.StartsWith("I"))
                {
                    interfaceName = interfaceName.Right(interfaceName.Length - 1);
                }

                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfaceType);
                }
            }

            return serviceTypes;
        }
    }
}
