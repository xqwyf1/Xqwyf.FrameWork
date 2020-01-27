using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.DependencyInjection;

using JetBrains.Annotations;

namespace Xqwyf.App
{
    /// <summary>
    /// 应用初始化时的上下文
    /// </summary>
    public class ApplicationInitializationContext : IServiceProviderAccessor
    {
        /// <summary>
        /// 应用初始化时的<see cref="IServiceProvider"/>
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }


        /// <summary>
        /// 创建一个<see cref="ApplicationInitializationContext"/>对象
        /// </summary>
        /// <param name="serviceProvider">应用初始化所拥有的<paramref name="serviceProvider"/></param>
        public ApplicationInitializationContext([NotNull] IServiceProvider serviceProvider)
        {
            XqCheck.NotNull(serviceProvider, nameof(serviceProvider));

            ServiceProvider = serviceProvider;
        }
    }
}
