using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace  Xqwyf.App
{
    /// <summary>
    /// IXqApplicationWithInternalServiceProvider实现
    /// </summary>
    internal class XqApplicationWithInternalServiceProvider : XqApplicationBase, IXqApplicationWithInternalServiceProvider
    {
       /// <summary>
       /// 本应用的<see cref="ServiceProvider"/>生命周期范围
       /// </summary>
        public IServiceScope ServiceScope { get; private set; }

        /// <summary>
        /// 创建一个具有内部<see cref="ServiceCollection"/>的应用提供者,并将当前对象加载入<see cref="ServiceCollection"/>
        /// </summary>
        /// <param name="startupModuleType">启动模块的类型</param>
        /// <param name="optionsAction">启动时，执行的方法</param>
        public XqApplicationWithInternalServiceProvider(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction
            ) : this(
            startupModuleType,
            new ServiceCollection(),
            optionsAction)
        {

        }

        /// <summary>
        /// 创建一个具有内部<see cref="ServiceCollection"/>的应用提供者,并将当前对象加载入<see cref="ServiceCollection"/>
        /// </summary>
        /// <param name="startupModuleType">启动模块的类型</param>
        /// <param name="services">启动时，所需的<see cref="IServiceCollection"/></param>
        /// <param name="optionsAction">启动时，执行的方法</param>
        private XqApplicationWithInternalServiceProvider(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction
            ) : base(
                startupModuleType,
                services,
                optionsAction)
        {
            Services.AddSingleton<IXqApplicationWithInternalServiceProvider>(this);
        }

        /// <summary>
        /// 执行<see cref="XqApplicationWithInternalServiceProvider"/>的初始化,
        /// 定义ServiceScope，设置<see cref="ServiceProvider"/>的生命周期
        /// </summary>
        public void Initialize()
        {
            ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
            SetServiceProvider(ServiceScope.ServiceProvider);

            InitializeModules();
        }

        public override void Dispose()
        {
            base.Dispose();
            ServiceScope.Dispose();
        }
    }
}
