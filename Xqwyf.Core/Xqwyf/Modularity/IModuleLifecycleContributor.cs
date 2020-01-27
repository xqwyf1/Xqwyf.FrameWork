using System;
using JetBrains.Annotations;
using Xqwyf.DependencyInjection;

using Xqwyf.App;


namespace Xqwyf.Modularity
{
    /// <summary>
    /// Module生命周期管理的定义，分为初始化，关闭
    /// </summary>
    public interface IModuleLifecycleContributor 
    {
        /// <summary>
        /// 应用初始化时
        /// </summary>
        /// <param name="context"></param>
        /// <param name="module"></param>
        void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] IXqModule module);


        /// <summary>
        /// 应用关闭时
        /// </summary>
        /// <param name="context"></param>
        /// <param name="module"></param>
        void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] IXqModule module);
    }
}
