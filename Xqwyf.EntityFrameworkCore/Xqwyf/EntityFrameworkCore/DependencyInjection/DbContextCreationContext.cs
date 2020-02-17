using System;
using System.Data.Common;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Xqwyf.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 创建<see cref="DbContext"/>时的上下文信息
    /// </summary>
    public class DbContextCreationContext
    {
        /// <summary>
        /// 当前已经创建<see cref="DbContextCreationContext"/>的上下文
        /// </summary>
        public static DbContextCreationContext Current => _current.Value;

        private static readonly AsyncLocal<DbContextCreationContext> _current = new AsyncLocal<DbContextCreationContext>();

        /// <summary>
        /// 用于<see cref="DbContext"/>的连接串名称
        /// </summary>
        public string ConnectionStringName { get; }

        /// <summary>
        /// 用于<see cref="DbContext"/>的连接串信息
        /// </summary>
        public string ConnectionString { get; }

        public DbConnection ExistingConnection { get; set; }

        /// <summary>
        /// 通过<paramref name="connectionString"/>和<paramref name="connectionStringName"/>创建一个<see cref="DbContextCreationContext"/>(DbContext创建时的上下文)
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <param name="connectionString"></param>
        public DbContextCreationContext(string connectionStringName, string connectionString)
        {
            ConnectionStringName = connectionStringName;
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 在此时，使用<paramref name="context"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IDisposable Use(DbContextCreationContext context)
        {
            var previousValue = Current;
            _current.Value = context;
            return new DisposeAction(() => _current.Value = previousValue);
        }
    }
}
