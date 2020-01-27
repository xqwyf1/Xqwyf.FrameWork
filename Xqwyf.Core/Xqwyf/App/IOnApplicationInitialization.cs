using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace Xqwyf.App
{
    /// <summary>
    /// 应用初始化接口
    /// </summary>
    public interface IOnApplicationInitialization
    {
        /// <summary>
        /// 应用初始化时的操作
        /// </summary>
        /// <param name="context">应用初始化时的上下文</param>
        void OnApplicationInitialization([NotNull] ApplicationInitializationContext context);
    }
}
