using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Xqwyf.Modularity;

namespace  Xqwyf.App
{
    /// <summary>
    /// 通过本工厂，创建一个应用，启动应用
    /// </summary>
    public static class XqApplicationFactory
    {
        /// <summary>
        /// 通过<typeparamref name="TStartupModule"/>，创建一个具有内部服务的应用提供者
        /// </summary>
        /// <typeparam name="TStartupModule">启动的模块的类型</typeparam>
        /// <param name="optionsAction">启动时，执行的Action</param>
        /// <returns>创建的<see cref="IXqApplicationWithInternalServiceProvider"/></returns>
        public static IXqApplicationWithInternalServiceProvider Create<TStartupModule>(
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IXqModule
        {
            return Create(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// 通过<paramref name="startupModuleType"/>,创建一个具有内部服务的应用提供者
        /// </summary>
        /// <param name="startupModuleType">启动模块的类型</param>
        /// <param name="optionsAction">启动时，执行的Action</param>
        /// <returns>创建的<see cref="IXqApplicationWithInternalServiceProvider"/></returns>
        public static IXqApplicationWithInternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
        {
            return new XqApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
        }

        /// <summary>
        /// 通过<typeparamref name="TStartupModule"/>，创建一个具有外部服务的应用提供者
        /// </summary>
        /// <typeparam name="TStartupModule">启动模块的类型</typeparam>
        /// <param name="services">被提供的<paramref name="IServiceCollection"/></param>
        /// <param name="optionsAction">启动时，执行的Action</param>
        /// <returns></returns>
        public static IXqApplicationWithExternalServiceProvider Create<TStartupModule>(
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IXqModule
        {
            return Create(typeof(TStartupModule), services, optionsAction);
        }

        /// <summary>
        /// 通过<typeparamref name="startupModuleType"/>，创建一个具有外部服务的应用提供者
        /// </summary>
        /// <typeparam name="TStartupModule">启动模块的类型</typeparam>
        /// <param name="services">被提供的<paramref name="IServiceCollection"/></param>
        /// <param name="optionsAction">启动时，执行的Action</param>
        /// <returns></returns>
        public static IXqApplicationWithExternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction = null)
        {
            return new XqApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
        }
    }
}
