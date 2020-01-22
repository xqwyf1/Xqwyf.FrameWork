using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace  Xqwyf
{

    /// <summary>
    /// 外部服务应用提供者,需要提供<see cref="IServiceProvider"/>，
    /// 本类在<see cref="XqApplicationFactory"/>中调用
    /// </summary>
    internal class XqApplicationWithExternalServiceProvider : XqApplicationBase, IXqApplicationWithExternalServiceProvider
    {
        /// <summary>
        /// 根据启动<paramref name="startupModuleType"/>,<paramref name="services"/>,<paramref name="optionsAction"/>，创建一个
        /// <see cref="XqApplicationWithExternalServiceProvider"/>对象，完成后，将应用对象载入<paramref name="services"/>
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        public XqApplicationWithExternalServiceProvider(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<XqApplicationCreationOptions> optionsAction
            ) : base(
                startupModuleType,
                services,
                optionsAction)
        {
            services.AddSingleton<IXqApplicationWithExternalServiceProvider>(this);
        }

        /// <summary>
        /// <see cref="XqApplicationWithExternalServiceProvider"/>的初始化，设置应用的<see cref="IServiceProvider"/>，进行Module的初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void Initialize(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            SetServiceProvider(serviceProvider);

            InitializeModules();
        }
    }
}
