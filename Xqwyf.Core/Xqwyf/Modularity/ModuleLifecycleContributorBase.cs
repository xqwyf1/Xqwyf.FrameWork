using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Modularity
{
using System;
    public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
    {
        public virtual void Initialize(ApplicationInitializationContext context, IXqModule module)
        {
        }

        public virtual void Shutdown(ApplicationShutdownContext context, IXqModule module)
        {
        }
    }
}
