using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Xqwyf.Data 
{
    /// <summary>
    /// ConnectionString解析器接口
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// 解析名称为<paramref name="connectionStringName"/>的连接串
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        [NotNull]
        string Resolve(string connectionStringName = null);
    }
}
