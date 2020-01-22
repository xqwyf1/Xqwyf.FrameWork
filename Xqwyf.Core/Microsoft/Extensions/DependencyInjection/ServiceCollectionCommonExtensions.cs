using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace  Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionCommonExtensions
    {
        /// <summary>
        /// 判断类型为<typeparamref name="T"/>的Service是否已经存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>

        public static bool IsAdded<T>(this IServiceCollection services)
        {
            return services.IsAdded(typeof(T));
        }

        /// <summary>
        /// 判断类型为<paramref name="type"/>的Service是否已经存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static bool IsAdded(this IServiceCollection services, Type type)
        {
            return services.Any(d => d.ServiceType == type);
        }

        /// <summary>
        /// 从<paramref name="services"/>获取<typeparamref name="T"/>的实例，如果为空，将抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        /// <summary>
        /// 从<paramref name="services"/>获取<typeparamref name="T"/>的实例，如果为空，会返回空值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }
    }
}
