using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.DependencyInjection;

namespace Xqwyf.Domain.Repositories
{
    /// <summary>
    /// 使用<typeparamref name="TOptions"/>注册仓储
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public abstract class RepositoryRegistrarBase<TOptions>
         where TOptions : XqCommonDbContextRegistrationOptions
    {
        /// <summary>
        /// 注册所使用的选项
        /// </summary>
        public TOptions Options { get; }

        /// <summary>
        /// 根据<paramref name="options"/>创建一个RepositoryRegistrar对象Base
        /// </summary>
        /// <param name="options"></param>
        protected RepositoryRegistrarBase(TOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// 添加仓储
        /// </summary>
        public virtual void AddRepositories()
        {
            foreach (var customRepository in Options.CustomRepositories)
            {
                Options.Services.AddDefaultRepository(customRepository.Key, customRepository.Value);
            }

            if (Options.RegisterDefaultRepositories)
            {
                RegisterDefaultRepositories();
            }
        }
    }
}
