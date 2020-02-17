﻿using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace  Xqwyf.Domain.EntityFrameworkCore.DependencyInjection
{
    public interface IXqCommonDbContextRegistrationOptionsBuilder
    {
        IServiceCollection Services { get; }

        /// <summary>
        /// Registers default repositories for this DbContext. 
        /// </summary>
        /// <param name="includeAllEntities">
        /// Registers repositories only for aggregate root entities by default.
        /// set <see cref="includeAllEntities"/> to true to include all entities.
        /// </param>
        IXqCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(bool includeAllEntities = false);

        /// <summary>
        /// Registers default repositories for this DbContext.
        /// Default repositories will use given <see cref="TDefaultRepositoryDbContext"/>.
        /// </summary>
        /// <typeparam name="TDefaultRepositoryDbContext">DbContext type that will be used by default repositories</typeparam>
        /// <param name="includeAllEntities">
        /// Registers repositories only for aggregate root entities by default.
        /// set <see cref="includeAllEntities"/> to true to include all entities.
        /// </param>
        IXqCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext>(bool includeAllEntities = false);

        /// <summary>
        /// Registers default repositories for this DbContext.
        /// Default repositories will use given <see cref="defaultRepositoryDbContextType"/>.
        /// </summary>
        /// <param name="defaultRepositoryDbContextType">DbContext type that will be used by default repositories</param>
        /// <param name="includeAllEntities">
        /// Registers repositories only for aggregate root entities by default.
        /// set <see cref="includeAllEntities"/> to true to include all entities.
        /// </param>
        IXqCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(Type defaultRepositoryDbContextType, bool includeAllEntities = false);

        /// <summary>
        /// Registers custom repository for a specific entity.
        /// Custom repositories overrides default repositories.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TRepository">Repository type</typeparam>
        IXqCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository>();

        /// <summary>
        /// Uses given class(es) for default repositories.
        /// </summary>
        /// <param name="repositoryImplementationType">Repository implementation type</param>
        /// <param name="repositoryImplementationTypeWithoutKey">Repository implementation type (without primary key)</param>
        /// <returns></returns>
        IXqCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses([NotNull] Type repositoryImplementationType, [NotNull] Type repositoryImplementationTypeWithoutKey);

        /// <summary>
        /// Replaces given DbContext type with this DbContext type.
        /// </summary>
        /// <typeparam name="TOtherDbContext">The DbContext type to be replaced</typeparam>
        IXqCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext>();

        /// <summary>
        /// Replaces given DbContext type with this DbContext type.
        /// </summary>
        /// <param name="otherDbContextType">The DbContext type to be replaced</param>
        IXqCommonDbContextRegistrationOptionsBuilder ReplaceDbContext(Type otherDbContextType);
    }
}
