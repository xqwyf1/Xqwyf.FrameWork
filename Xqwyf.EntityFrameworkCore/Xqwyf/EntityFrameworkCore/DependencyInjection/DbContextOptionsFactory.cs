using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xqwyf.Data;

namespace  Xqwyf.EntityFrameworkCore.DependencyInjection
{
    internal static class DbContextOptionsFactory
    {
        /// <summary>
        /// 创建用于<typeparamref name="TDbContext"/>的<see cref="DbContextOptions"/>上下文
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static DbContextOptions<TDbContext> Create<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : XqDbContext<TDbContext>
        {
            var creationContext = GetCreationContext<TDbContext>(serviceProvider);

            var context = new XqDbContextConfigurationContext<TDbContext>(
                creationContext.ConnectionString,
                serviceProvider,
                creationContext.ConnectionStringName,
                creationContext.ExistingConnection
            );

            var options = GetDbContextOptions<TDbContext>(serviceProvider);

            PreConfigure(options, context);
            Configure(options, context);

            return context.DbContextOptions.Options;
        }
        /// <summary>
        /// 进行用于<typeparamref name="TDbContext"/>的预配置
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="options"></param>
        /// <param name="context"></param>
        private static void PreConfigure<TDbContext>(
            XqDbContextOptions options,
            XqDbContextConfigurationContext<TDbContext> context)
            where TDbContext : XqDbContext<TDbContext>
        {
            foreach (var defaultPreConfigureAction in options.DefaultPreConfigureActions)
            {
                defaultPreConfigureAction.Invoke(context);
            }

            var preConfigureActions = options.PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (!preConfigureActions.IsNullOrEmpty())
            {
                foreach (var preConfigureAction in preConfigureActions)
                {
                    ((Action<XqDbContextConfigurationContext<TDbContext>>)preConfigureAction).Invoke(context);
                }
            }
        }

        /// <summary>
        /// 进行用于<typeparamref name="TDbContext"/>的配置
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="options"></param>
        /// <param name="context"></param>
        private static void Configure<TDbContext>(
            XqDbContextOptions options,
            XqDbContextConfigurationContext<TDbContext> context)
            where TDbContext : XqDbContext<TDbContext>
        {
            var configureAction = options.ConfigureActions.GetOrDefault(typeof(TDbContext));
            if (configureAction != null)
            {
                ((Action<XqDbContextConfigurationContext<TDbContext>>)configureAction).Invoke(context);
            }
            else if (options.DefaultConfigureAction != null)
            {
                options.DefaultConfigureAction.Invoke(context);
            }
            else
            {
                throw new XqException(
                    $"No configuration found for {typeof(DbContext).AssemblyQualifiedName}! Use services.Configure<XqDbContextOptions>(...) to configure it.");
            }
        }

        /// <summary>
        /// 通过<paramref name="serviceProvider"/>获取一个用于<typeparamref name="TDbContext"/>的Options
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static XqDbContextOptions GetDbContextOptions<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : XqDbContext<TDbContext>
        {
            return serviceProvider.GetRequiredService<IOptions<XqDbContextOptions>>().Value;
        }


        /// <summary>
        /// 获取<see cref="DbContextCreationContext"/>中的当前实例，如果没有创建，则根据进行创建
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static DbContextCreationContext GetCreationContext<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : XqDbContext<TDbContext>
        {
            var context = DbContextCreationContext.Current;
            if (context != null)
            {
                return context;
            }

            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
            var connectionString = serviceProvider.GetRequiredService<IConnectionStringResolver>().Resolve(connectionStringName);

            return new DbContextCreationContext(
                connectionStringName,
                connectionString
            );
        }
    }
}
