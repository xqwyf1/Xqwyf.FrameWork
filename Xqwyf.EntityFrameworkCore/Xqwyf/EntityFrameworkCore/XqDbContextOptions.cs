using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xqwyf.EntityFrameworkCore.DependencyInjection;

namespace  Xqwyf.EntityFrameworkCore
{
    /// <summary>
    /// 用于<see cref="XqDbContext{TDbContext}"/>的配置处理
    /// </summary>
    public class XqDbContextOptions
    {
        /// <summary>
        /// 获取或者设置默认的预配置操作
        /// </summary>
        internal List<Action<XqDbContextConfigurationContext>> DefaultPreConfigureActions { get; set; }

        /// <summary>
        /// 获取或者设置默认的配置操作
        /// </summary>
        internal Action<XqDbContextConfigurationContext> DefaultConfigureAction { get; set; }

        /// <summary>
        ///获取或者设置预配置操作
        /// </summary>
        internal Dictionary<Type, List<object>> PreConfigureActions { get; set; }

        /// <summary>
        /// 获取或者设置配置时操作
        /// </summary>
        internal Dictionary<Type, object> ConfigureActions { get; set; }

        /// <summary>
        /// 创建一个<see cref="XqDbContextOptions"/>，并进行初始化
        /// </summary>
        public XqDbContextOptions()
        {
            DefaultPreConfigureActions = new List<Action<XqDbContextConfigurationContext>>();
            PreConfigureActions = new Dictionary<Type, List<object>>();
            ConfigureActions = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 在<see cref="DefaultPreConfigureActions"/>中注册预配置时使用的默认<paramref name="action"/>
        /// </summary>
        /// <param name="action"></param>
        public void PreConfigure([NotNull] Action<XqDbContextConfigurationContext> action)
        {
            XqCheck.NotNull(action, nameof(action));

            DefaultPreConfigureActions.Add(action);
        }

        /// <summary>
        /// 在<see cref="DefaultConfigureAction"/>中注册配置时使用的默认<paramref name="action"/>
        /// </summary>
        /// <param name="action"></param>
        public void Configure([NotNull] Action<XqDbContextConfigurationContext> action)
        {
            XqCheck.NotNull(action, nameof(action));

            DefaultConfigureAction = action;
        }

        /// <summary>
        /// 注册类型为<typeparamref name="TDbContext"/>的预配置
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="action"></param>
        public void PreConfigure<TDbContext>([NotNull] Action<XqDbContextConfigurationContext<TDbContext>> action)
            where TDbContext : XqDbContext<TDbContext>
        {
            XqCheck.NotNull(action, nameof(action));

            var actions = PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (actions == null)
            {
                PreConfigureActions[typeof(TDbContext)] = actions = new List<object>();
            }

            actions.Add(action);
        }

        public void Configure<TDbContext>([NotNull] Action<XqDbContextConfigurationContext<TDbContext>> action)
            where TDbContext : XqDbContext<TDbContext>
        {
            XqCheck.NotNull(action, nameof(action));

            ConfigureActions[typeof(TDbContext)] = action;
        }
    }
}
