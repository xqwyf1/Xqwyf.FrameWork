using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.App;


namespace Xqwyf.Modularity
{
    public class OnApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        public override void Initialize(ApplicationInitializationContext context, IXqModule module)
        {
            (module as IOnApplicationInitialization)?.OnApplicationInitialization(context);
        }
    }

    public class OnApplicationShutdownModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        /// <summary>
        /// <paramref name="module"/>注销时，调用该模块的OnApplicationShutdown操作，进行注销
        /// </summary>
        /// <param name="context"></param>
        /// <param name="module"></param>
        public override void Shutdown(ApplicationShutdownContext context, IXqModule module)
        {
            (module as IOnApplicationShutdown)?.OnApplicationShutdown(context);
        }
    }

    public class OnPreApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        public override void Initialize(ApplicationInitializationContext context, IXqModule module)
        {
            (module as IOnPreApplicationInitialization)?.OnPreApplicationInitialization(context);
        }
    }

    public class OnPostApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        public override void Initialize(ApplicationInitializationContext context, IXqModule module)
        {
            (module as IOnPostApplicationInitialization)?.OnPostApplicationInitialization(context);
        }
    }
}
