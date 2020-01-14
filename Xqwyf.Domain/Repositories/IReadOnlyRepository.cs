using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

using Xqwyf.Domain.Entities;


namespace Xqwyf.Domain.Repositories
{
    /// <summary>
    /// 只读仓储接口
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合类型</typeparam>
    public interface IReadOnlyRepository<TEntity> : IRepository, IQueryable<TEntity>
       where TEntity : class, IEntity
    {
        /// <summary>
        /// 获取所有的聚合根列表
        /// </summary>
        /// <param name="includeDetails">说明是否包括子实体，默认不包括</param>
        /// <param name="cancellationToken">说明操作是否可以取消</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取聚合根个数
        /// </summary>
        /// <param name="cancellationToken">说明操作是否可以取消</param>
        /// <returns></returns>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据给定的主键获取实体
        /// 如果根据主键，没有发现实体，抛出<see cref="EntityNotFoundException"/>
        /// </summary>
        /// <param name="id">获取实体的主键</param>
        /// <param name="includeDetails">是否包括子实体</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entity</returns>
        [NotNull]
        Task<TEntity> GetAsync(object id, bool includeDetails = true, CancellationToken cancellationToken = default);


        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <param name="includeDetails">Set true to include all children of this entity</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>Entity or null</returns>
        Task<TEntity> FindAsync(object id, bool includeDetails = true, CancellationToken cancellationToken = default);


        IQueryable<TEntity> WithDetails();

        IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);
    }
}
