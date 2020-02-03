using System;
using System.Collections.Generic;
using System.Text;


namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 主键标记，表示某个属性为主键，只能用于Ientity
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    sealed class PriKeyAttribute : Attribute
    {
        public PriKeyAttribute()
        {
                
        }
    }
}
