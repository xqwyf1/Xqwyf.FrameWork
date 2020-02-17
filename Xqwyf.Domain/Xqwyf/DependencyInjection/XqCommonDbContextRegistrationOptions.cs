using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Repositories;

namespace Xqwyf.DependencyInjection
{
    /// <summary>
    /// DbContext的注册时的选项，为Builder
    /// </summary>
    public abstract class XqCommonDbContextRegistrationOptions:IXqCommonDbContextRegistrationOptionsBuilder
    {
        /// <summary>
        /// 仓储当前默认的DbContext类型
        /// </summary>
        public Type DefaultRepositoryDbContextType { get; protected set; }


        /// <summary>
        /// 仓储最初的DbContext类型
        /// </summary>
        public Type OriginalDbContextType { get; }

        /// <summary>
        /// 使用到的服务集合
        /// </summary>
        public IServiceCollection Services { get; }
        /// <summary>
        /// 需要进行自定义仓储的实体类型和仓储类型
        /// </summary>
        public Dictionary<Type, Type> CustomRepositories { get; }

        public Type DefaultRepositoryImplementationType { get; private set; }

        public Type DefaultRepositoryImplementationTypeWithoutKey { get; private set; }

        public bool RegisterDefaultRepositories { get; private set; }


        public bool IncludeAllEntitiesForDefaultRepositories { get; private set; }

        public List<Type> ReplacedDbContextTypes { get; }

        #region 构造函数

        /// <summary>
        /// 创建一个XqCommonDbContextRegistrationOptions
        /// </summary>
        /// <param name="originalDbContextType">默认的DbContext类型</param>
        /// <param name="services">本DbContext的注册选项构造器使用的Services</param>
        protected XqCommonDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        {
            OriginalDbContextType = originalDbContextType;
            Services = services;
            DefaultRepositoryDbContextType = originalDbContextType;
            CustomRepositories = new Dictionary<Type, Type>();
            ReplacedDbContextTypes = new List<Type>();
        }
        #endregion


        public IXqCommonDbContextRegistrationOptionsBuilder ReplaceDbContextOption<TOtherDbContext>()
        {
            return ReplaceDbContextOption(typeof(TOtherDbContext));
        }


        public IXqCommonDbContextRegistrationOptionsBuilder ReplaceDbContextOption(Type otherDbContextType)
        {
            if (!otherDbContextType.IsAssignableFrom(OriginalDbContextType))
            {
                throw new XqException($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {otherDbContextType.AssemblyQualifiedName}!");
            }

            ReplacedDbContextTypes.Add(otherDbContextType);

            return this;
        }

        public IXqCommonDbContextRegistrationOptionsBuilder AddRepositoryOption<TEntity, TRepository>()
        {
            AddCustomRepositoryOption(typeof(TEntity), typeof(TRepository));

            return this;
        }

        public IXqCommonDbContextRegistrationOptionsBuilder AddDefaultRepositoriesOption<TDefaultRepositoryDbContext>(bool includeAllEntities = false)
        {
            return AddDefaultRepositoriesOption(typeof(TDefaultRepositoryDbContext), includeAllEntities);
        }

        public IXqCommonDbContextRegistrationOptionsBuilder AddDefaultRepositoriesOption(Type defaultRepositoryDbContextType, bool includeAllEntities = false)
        {
            if (!defaultRepositoryDbContextType.IsAssignableFrom(OriginalDbContextType))
            {
                throw new XqException($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {defaultRepositoryDbContextType.AssemblyQualifiedName}!");
            }

            DefaultRepositoryDbContextType = defaultRepositoryDbContextType;

            return AddDefaultRepositoriesOption(includeAllEntities);
        }

        public IXqCommonDbContextRegistrationOptionsBuilder AddDefaultRepositoriesOption(bool includeAllEntities = false)
        {
            RegisterDefaultRepositories = true;
            IncludeAllEntitiesForDefaultRepositories = includeAllEntities;

            return this;
        }

        public IXqCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClassesOption(
          Type repositoryImplementationType,
          Type repositoryImplementationTypeWithoutKey
          )
        {
            XqCheck.NotNull(repositoryImplementationType, nameof(repositoryImplementationType));
            XqCheck.NotNull(repositoryImplementationTypeWithoutKey, nameof(repositoryImplementationTypeWithoutKey));

            DefaultRepositoryImplementationType = repositoryImplementationType;
            DefaultRepositoryImplementationTypeWithoutKey = repositoryImplementationTypeWithoutKey;

            return this;
        }

        /// <summary>
        /// 添加自定义仓储设置
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="repositoryType"></param>
        private void AddCustomRepositoryOption(Type entityType, Type repositoryType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new XqException($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntity).AssemblyQualifiedName}.");
            }

            if (!typeof(IRepository).IsAssignableFrom(repositoryType))
            {
                throw new XqException($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IBasicRepository<>).AssemblyQualifiedName}.");
            }
            CustomRepositories[entityType] = repositoryType;
        }
    }
}
