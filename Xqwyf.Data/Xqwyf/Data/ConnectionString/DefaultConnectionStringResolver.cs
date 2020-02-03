using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Xqwyf.DependencyInjection;

namespace Xqwyf.Data 
{
    /// <summary>
    /// 默认的连接串解析器
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        protected XqDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver(IOptionsSnapshot<XqDbConnectionOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 解析名称为指定的<paramref name="connectionStringName"/>的连接串
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public virtual string Resolve(string connectionStringName = null)
        {
            //Get module specific value if provided
            if (!connectionStringName.IsNullOrEmpty())
            {
                var moduleConnString = Options.ConnectionStrings.GetOrDefault(connectionStringName);
                if (!moduleConnString.IsNullOrEmpty())
                {
                    return moduleConnString;
                }
            }

            //Get default value
            return Options.ConnectionStrings.Default;
        }
    }
}
