using Xqwyf.Collections;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// <see cref="XqModule"/>的生命周期选项
    /// </summary>
    public class XqModuleLifecycleOptions
    {
        public ITypeList<IModuleLifecycleContributor> Contributors { get; }

        public XqModuleLifecycleOptions()
        {
            Contributors = new TypeList<IModuleLifecycleContributor>();
        }
    }
}
