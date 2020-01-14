using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Xqwyf.Domain.Entities;

namespace Xqwyf.Domain.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBasicRepository<TEntity> : IReadOnlyRepository<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 插入一个聚合
        /// </summary>
        /// <param name="autoSave">
        /// Set true to automatically save changes to database.
        /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <param name="entity">Inserted entity</param>
        [NotNull]
        Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        ///修改一个聚合
        /// </summary>
        /// <param name="autoSave">
        /// Set true to automatically save changes to database.
        /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <param name="entity">Entity</param>
        [NotNull]
        Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        ///根据删除一个聚合
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        /// <param name="autoSave">
        /// Set true to automatically save changes to database.
        /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <param name="autoSave">
        /// Set true to automatically save changes to database.
        /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task DeleteAsync(object id, bool autoSave = false, CancellationToken cancellationToken = default);  //TODO: Return true if deleted

    }
}
