using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Core.Guids
{
    /// <summary>
    /// 简单的Guid生成器
    /// </summary>
    class SimpleGuidGenerator : IGuidGenerator
    {
        public static SimpleGuidGenerator Instance { get; } = new SimpleGuidGenerator();

        public Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
