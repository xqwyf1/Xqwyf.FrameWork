using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Xqwyf.Domain.Entities;
using Xqwyf.Threading;

namespace Xqwyf.Domain.Repositories
{

    /// <summary>
    /// 只读仓储实现
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public abstract class ReadOnlyRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
         where TAggregateRoot : class, IAggregateRoot
    {

        protected virtual CancellationToken GetCancellationToken(CancellationToken preferredValue = default)
        {
            return CancellationTokenProvider.FallbackToProvider(preferredValue);
        }

        public ICancellationTokenProvider CancellationTokenProvider { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        #region  IQueryable实现
        /// <summary>
        /// 逐个返回聚合的枚举
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 逐个返回聚合的枚举
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TAggregateRoot> GetEnumerator()
        {
            return GetQueryable().GetEnumerator();
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<TAggregateRoot> GetQueryable();

        /// <summary>
        /// 获取聚合的元素类型
        /// </summary>
        public virtual Type ElementType => GetQueryable().ElementType;

        /// <summary>
        /// 获取IQueryable的相关表达式
        /// </summary>
        public virtual Expression Expression => GetQueryable().Expression;

        /// <summary>
        /// IQueryable来源
        /// </summary>
        public virtual IQueryProvider Provider => GetQueryable().Provider;

        #endregion

        #region IReadOnlyRepository 实现

        public abstract Task<TAggregateRoot> FindAsync(object id, bool includeDetails = true, CancellationToken cancellationToken = default);

        public abstract Task<List<TAggregateRoot>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);

        public abstract Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        public virtual async Task<TAggregateRoot> GetAsync(object id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TAggregateRoot), id);
            }

            return entity;
        }

        public virtual IQueryable<TAggregateRoot> WithDetails()
        {
            return GetQueryable();
        }

        public virtual IQueryable<TAggregateRoot> WithDetails(params Expression<Func<TAggregateRoot, object>>[] propertySelectors)
        {
            return GetQueryable();
        }

        #endregion
    }
}
