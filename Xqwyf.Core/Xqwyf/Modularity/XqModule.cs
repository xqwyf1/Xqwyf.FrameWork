using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using Xqwyf.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
        /// 是否忽略服务注册
        /// </summary>
        protected internal bool SkipAutoServiceRegistration { get; protected set; }
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
        internal static void CheckXqModuleType(Type moduleType)
        {
            if (!IsXqModule(moduleType))
            {
                throw new ArgumentException("Given type is not an Xq module: " + moduleType.AssemblyQualifiedName);
            }
        }

        /// <summary>
        /// 注册用于配置类型为<typeparamref name="TOptions"/>的操作。
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="configureOptions"></param>
        protected void Configure<TOptions>(Action<TOptions> configureOptions)
         where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(configureOptions);
        }

        /// <summary>
        /// 注册用于配置类型为<typeparamref name="TOptions"/>并且Options名称为<paramref name="name"/>的操作。
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="name"></param>
        /// <param name="configureOptions"></param>
        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
          where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(name, configureOptions);
        }

        /// <summary>
        /// 注册将与<typeparamref name="TOptions"/>绑定的<paramref name="configuration"/>实例。
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="configuration"></param>
        protected void Configure<TOptions>(IConfiguration configuration)
           where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
        }

        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
          where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
        }

        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
        }

        protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PreConfigure(configureOptions);
        }

 

        protected void PostConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PostConfigure(configureOptions);
        }

        protected void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PostConfigureAll(configureOptions);
        }

        /// <summary>
        /// 所有服务配置的上下文，服务配置完成后，将被设置为Null
        /// </summary>
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
