using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace  Xqwyf.DependencyInjection
{
    /// <summary>
    /// 常规注册，注册程序集，类型等
    /// </summary>
    public interface IConventionalRegistrar
    {
        /// <summary>
        /// 在服务中注册<paramref name="assembly"/>中所有<see cref="Type"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        void AddAssembly(IServiceCollection services, Assembly assembly);

        /// <summary>
        /// 在服务中注册多个<see cref="Type"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        void AddTypes(IServiceCollection services, params Type[] types);

        /// <summary>
        /// 在服务中注册<paramref name="type"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        void AddType(IServiceCollection services, Type type);
    }
}
