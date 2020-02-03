using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Auditing
{
    /// <summary>
    /// 实体的属性变更记录
    /// </summary>
    [Serializable]
    public class EntityPropertyChangeInfo
    {
        public const int MaxPropertyNameLength = 96;

      
        public const int MaxValueLength = 512;

        public const int MaxPropertyTypeFullNameLength = 192;

        /// <summary>
        /// 新值
        /// </summary>
        public virtual string NewValue { get; set; }


        /// <summary>
        /// 原值
        /// </summary>
        public virtual string OriginalValue { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public virtual string PropertyName { get; set; }

        public virtual string PropertyTypeFullName { get; set; }
    }
}
