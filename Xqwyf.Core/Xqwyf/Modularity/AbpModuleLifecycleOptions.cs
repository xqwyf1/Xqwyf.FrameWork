using Xqwyf.Collections;

namespace  Xqwyf.Modularity
{
    public class XqModuleLifecycleOptions
    {
        public ITypeList<IModuleLifecycleContributor> Contributors { get; }

        public XqModuleLifecycleOptions()
        {
            Contributors = new TypeList<IModuleLifecycleContributor>();
        }
    }
}
