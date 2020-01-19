using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Guids
{
    /// <summary>
    /// 简单的Guid生成器
    /// </summary>
   public class SimpleGuidGenerator : IGuidGenerator
    {
        public static SimpleGuidGenerator Instance { get; } = new SimpleGuidGenerator();

        public Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
