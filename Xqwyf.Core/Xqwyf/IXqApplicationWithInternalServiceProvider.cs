using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf
{
    /// <summary>
    /// 应用提供者，不需要提供<see cref="IServiceProvider"/>
    /// </summary>
    public interface IXqApplicationWithInternalServiceProvider : IXqApplication
    {
        /// <summary>
        /// 应用初始化
        /// </summary>
        void Initialize();
    }
}
