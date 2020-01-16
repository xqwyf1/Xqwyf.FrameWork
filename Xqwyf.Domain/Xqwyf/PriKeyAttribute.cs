using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain
{
    /// <summary>
    /// 主键标记
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    sealed class PriKeyAttribute : Attribute
    {
        public PriKeyAttribute()
        {
  
        }

    }
}
