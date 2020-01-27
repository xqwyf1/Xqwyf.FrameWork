using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace  Xqwyf.App
{

    /// <summary>
    /// 应用关闭时的上下文,只用于应用关闭时
    /// </summary>
    public class ApplicationShutdownContext
    {
        /// <summary>
        /// 应用关闭时相关的服务列表
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        public ApplicationShutdownContext([NotNull] IServiceProvider serviceProvider)
        {
            XqCheck.NotNull(serviceProvider, nameof(serviceProvider));

            ServiceProvider = serviceProvider;
        }
    }
}
