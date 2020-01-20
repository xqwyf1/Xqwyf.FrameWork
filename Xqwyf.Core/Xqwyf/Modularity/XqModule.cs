using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 本框架中所有模块的基础类
    /// </summary>
    public abstract class XqModule :
        IXqModule,
        IOnPreApplicationInitialization,
        IOnApplicationInitialization,
        IOnPostApplicationInitialization,
        IOnApplicationShutdown,
        IPreConfigureServices,
        IPostConfigureServices
    {

        #region 接口实现

        #region IPreConfigureServices实现

        public virtual void PreConfigureServices(ServiceConfigurationContext context)
        {

        }
        #endregion

        #region IAbpModule实现

        public virtual void ConfigureServices(ServiceConfigurationContext context)
        {

        }
        #endregion

        #region         IPostConfigureServices实现

        public virtual void PostConfigureServices(ServiceConfigurationContext context)
        {

        }
        #endregion

        #region IOnApplicationInitialization实现
        public virtual void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }

        #endregion

        #region IOnPostApplicationInitialization实现

       
        public virtual void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {

        }

        #endregion

        #region IOnPreApplicationInitialization实现


        public virtual void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {

        }

        #endregion

        #region IOnApplicationShutdown实现

        public virtual void OnApplicationShutdown(ApplicationShutdownContext context)
        {

        }


        #endregion

        #endregion

        /// <summary>
        /// 判断当前对象是否为<see cref="XqModule"/>
        /// </summary>
        /// <param name="type">被判断的类型</param>
        /// <returns></returns>
        public static bool IsXqModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IXqModule).GetTypeInfo().IsAssignableFrom(type);
        }

        /// <summary>
        /// 检查对象是否为<see cref="XqModule"/>，如果不是就抛出异常
        /// </summary>
        /// <param name="moduleType">被检查的类型</param>
        internal static void CheckAbpModuleType(Type moduleType)
        {
            if (!IsXqModule(moduleType))
            {
                throw new ArgumentException("Given type is not an Xq module: " + moduleType.AssemblyQualifiedName);
            }
        }

        protected void Configure<TOptions>(Action<TOptions> configureOptions)
         where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(configureOptions);
        }

        protected internal ServiceConfigurationContext ServiceConfigurationContext
        {
            get
            {
                if (_serviceConfigurationContext == null)
                {
                    throw new XqException($"{nameof(ServiceConfigurationContext)} is only available in the {nameof(ConfigureServices)}, {nameof(PreConfigureServices)} and {nameof(PostConfigureServices)} methods.");
                }

                return _serviceConfigurationContext;
            }
            internal set => _serviceConfigurationContext = value;
        }

        private ServiceConfigurationContext _serviceConfigurationContext;


    }
}
