using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.Modularity;
using Xqwyf.Guids;

namespace  Xqwyf.Domain
{
    /// <summary>
    /// 领域服务模块
    /// </summary>
    [DependsOn(
     typeof(XqGuidsModule)
     )]
    public class XqDomainModule : XqModule
    {

    }
}
