using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Core.Guids
{
    /// <summary>
    /// 用于生成Guid的接口
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// 创建一个新的<see cref="Guid"/>.
        /// </summary>
        Guid Create();
    }
}
