using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Data
{
    /// <summary>
    /// 数据库连接串字典
    /// </summary>
    [Serializable]
    public class ConnectionStrings : Dictionary<string, string>
    {
        public const string DefaultConnectionStringName = "Default";

        /// <summary>
        /// 默认的连接串
        /// </summary>
        public string Default
        {
            get => this.GetOrDefault(DefaultConnectionStringName);
            set => this[DefaultConnectionStringName] = value;
        }
    }
}
