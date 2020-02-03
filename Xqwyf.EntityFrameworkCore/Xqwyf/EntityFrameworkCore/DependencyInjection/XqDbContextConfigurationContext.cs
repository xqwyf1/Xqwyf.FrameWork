using System;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xqwyf.DependencyInjection;

namespace  Xqwyf.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 进行<see cref="XqDbContext{TDbContext}"/>的配置时的上线文
    /// </summary>
    public class XqDbContextConfigurationContext : IServiceProviderAccessor
    {
        /// <summary>
        /// 获取<see cref="IServiceProvider"/>
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 获取配置文件中的 连接串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 获取配置文件中的 连接串名称
        /// </summary>
        public string ConnectionStringName { get; }

        /// <summary>
        /// 获取当前的<see cref="DbConnection"/>
        /// </summary>
        public DbConnection ExistingConnection { get; }

        /// <summary>
        /// 获取或者设置<see cref="DbContextOptionsBuilder"/>
        /// </summary>
        public DbContextOptionsBuilder DbContextOptions { get; protected set; }

        public XqDbContextConfigurationContext(
            [NotNull] string connectionString,
            [NotNull] IServiceProvider serviceProvider,
            [CanBeNull] string connectionStringName,
            [CanBeNull]DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ServiceProvider = serviceProvider;
            ConnectionStringName = connectionStringName;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder()
                .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
        }
    }

    /// <summary>
    /// 用于<typeparamref name="TDbContext"/>的<see cref="XqDbContextConfigurationContext"/>
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class XqDbContextConfigurationContext<TDbContext> : XqDbContextConfigurationContext
        where TDbContext : XqDbContext<TDbContext>
    {
        public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>)base.DbContextOptions;

        /// <summary>
        /// 创建一个用于<typeparamref name="TDbContext"/>的<see cref="XqDbContextConfigurationContext"/>上下文
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionStringName"></param>
        /// <param name="existingConnection"></param>
        public XqDbContextConfigurationContext(
            string connectionString,
            [NotNull] IServiceProvider serviceProvider,
            [CanBeNull] string connectionStringName,
            [CanBeNull] DbConnection existingConnection)
            : base(
                  connectionString,
                  serviceProvider,
                  connectionStringName,
                  existingConnection)
        {
            base.DbContextOptions = new DbContextOptionsBuilder<TDbContext>()
                .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
        }
    }
}
