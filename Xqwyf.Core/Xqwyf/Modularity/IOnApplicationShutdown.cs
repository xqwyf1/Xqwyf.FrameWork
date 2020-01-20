using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Xqwyf.Modularity
{
    /// <summary>
    /// 应用关闭时的操作
    /// </summary>
    public interface IOnApplicationShutdown
    {
        void OnApplicationShutdown([NotNull] ApplicationShutdownContext context);
    }
}
