using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.DependencyInjection;
using System.Reflection;

namespace  Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionConventionalRegistrationExtensions
    {
        /// <summary>
        /// 服务中添加<paramref name="assembly"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssembly(this IServiceCollection services, Assembly assembly)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddAssembly(services, assembly);
            }

            return services;
        }

        internal static List<IConventionalRegistrar> GetConventionalRegistrars(this IServiceCollection services)
        {
            return GetOrCreateRegistrarList(services);
        }

        private static ConventionalRegistrarList GetOrCreateRegistrarList(IServiceCollection services)
        {
            var conventionalRegistrars = services.GetSingletonInstanceOrNull<IObjectAccessor<ConventionalRegistrarList>>()?.Value;
            if (conventionalRegistrars == null)
            {
                conventionalRegistrars = new ConventionalRegistrarList { new DefaultConventionalRegistrar() };
                services.AddObjectAccessor(conventionalRegistrars);
            }

            return conventionalRegistrars;
        }

        /// <summary>
        /// 在服务中增加<typeparamref name="T"/>所在的程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyOf<T>(this IServiceCollection services)
        {
            return services.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }


        public static IServiceCollection AddTypes(this IServiceCollection services, params Type[] types)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddTypes(services, types);
            }

            return services;
        }

        public static IServiceCollection AddType<TType>(this IServiceCollection services)
        {
            return services.AddType(typeof(TType));
        }

        public static IServiceCollection AddType(this IServiceCollection services, Type type)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddType(services, type);
            }

            return services;
        }
    }
}
