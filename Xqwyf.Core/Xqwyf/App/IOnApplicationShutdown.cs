using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Xqwyf.App
{
    /// <summary>
    /// 应用关闭时的操作
    /// </summary>
    public interface IOnApplicationShutdown
    {
        void OnApplicationShutdown([NotNull] ApplicationShutdownContext context);
    }
}
