using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Xqwyf.Modularity;

namespace  Xqwyf.App
{

    /// <summary>
    /// 应用的接口，表示一个应用对象,包括所有的<see cref="XqModule"/>
    /// </summary>
    public interface IXqApplication : IModuleContainer, IDisposable
    {
        /// <summary>
        /// 本应用的启动模块 
        /// </summary>
        Type StartupModuleType { get; }

        /// <summary>
        /// 注册到这个应用的所有Service，应用初始化完成后，不能增加服务
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        ///本应用的 service provider，应用初始化完成前，不能使用
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 应用关闭，通过<see cref="IModuleManager"/>进行逐个模块注销
        /// </summary>
        void Shutdown();
    }
}
