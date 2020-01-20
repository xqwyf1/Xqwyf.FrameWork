using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace  Xqwyf.Modularity
{
    /// <summary>
    /// 准备进行应用初始化时的接口
    /// </summary>
    public interface IOnPreApplicationInitialization
    {
        /// <summary>
        /// 准备进行应用初始化时的操作
        /// </summary>
        /// <param name="context"></param>
        void OnPreApplicationInitialization([NotNull] ApplicationInitializationContext context);
    }
}
