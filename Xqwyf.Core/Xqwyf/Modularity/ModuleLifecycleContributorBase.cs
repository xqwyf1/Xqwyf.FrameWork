using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.App;

namespace Xqwyf.Modularity
{

    public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
    {
        /// <summary>
        /// <paramref name="module"/>在初始时，根据<see cref="ApplicationInitializationContext"/>,进行的初始化操作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="module"></param>
        public virtual void Initialize(ApplicationInitializationContext context, IXqModule module)
        {
        }

        public virtual void Shutdown(ApplicationShutdownContext context, IXqModule module)
        {
        }
    }
}
