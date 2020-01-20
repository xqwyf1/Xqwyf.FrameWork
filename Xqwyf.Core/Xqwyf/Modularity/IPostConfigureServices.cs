using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Modularity
{
    public interface IPostConfigureServices
    {
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
