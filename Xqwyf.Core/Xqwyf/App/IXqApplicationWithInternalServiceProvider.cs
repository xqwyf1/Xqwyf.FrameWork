using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.App
{
    /// <summary>
    /// 应用提供者接口，本提供者不需要使用<see cref="IServiceProvider"/>,使用内部的<see cref="IServiceProvider"/>
    /// </summary>
    public interface IXqApplicationWithInternalServiceProvider : IXqApplication
    {
        /// <summary>
        /// 应用初始化
        /// </summary>
        void Initialize();
    }
}
