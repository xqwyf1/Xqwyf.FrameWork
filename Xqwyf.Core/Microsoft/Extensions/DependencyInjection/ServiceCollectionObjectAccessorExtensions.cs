using System;
using System.Linq;
using Xqwyf.DependencyInjection;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionObjectAccessorExtensions
    {
        /// <summary>
        /// 从服务中，获取类型为<typeparamref name="T"/>的对象访问器,如果还未创建，创建后返回对象访问器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ObjectAccessor<T> TryAddObjectAccessor<T>(this IServiceCollection services)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                return services.GetSingletonInstance<ObjectAccessor<T>>();
            }

            return services.AddObjectAccessor<T>();
        }

        /// <summary>
        /// 创建类型为<typeparamref name="T"/>的对象访问器，添加到服务中，并且返回所创建的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>());
        }

        /// <summary>
        /// 在服务中增加泛型为<typeparamref name="T"/>的<paramref name="accessor"/>,同时增加<see cref="IObjectAccessor{T}"/>和<see cref="ObjectAccessor{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, ObjectAccessor<T> accessor)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                throw new Exception("An object accessor is registered before for type: " + typeof(T).AssemblyQualifiedName);
            }

            //Add to the beginning for fast retrieve
            services.Insert(0, ServiceDescriptor.Singleton(typeof(ObjectAccessor<T>), accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));

            return accessor;
        }
    }
}
