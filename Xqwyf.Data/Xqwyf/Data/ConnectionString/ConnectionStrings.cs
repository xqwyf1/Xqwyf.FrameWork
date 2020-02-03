using System;
using System.Collections.Generic;

namespace  Xqwyf.Data
{
    /// <summary>
    /// 数据库连接串集合
    /// </summary>
    [Serializable]
    public class ConnectionStrings : Dictionary<string, string>
    {
        public const string DefaultConnectionStringName = "Default";

        /// <summary>
        /// 获取或者设置默认的连接串
        /// </summary>
        public string Default
        {
            get => this.GetOrDefault(DefaultConnectionStringName);
            set => this[DefaultConnectionStringName] = value;
        }
    }
}
