using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Repositories;
using Xqwyf.EntityFrameworkCore;
using Xqwyf.Domain.EntityFrameworkCore.DependencyInjection;

namespace Xqwyf.Domain.Repositories.EntityFrameworkCore
{
    /// <summary>
    /// IEfCoreRepository实现，为用于Ef的仓储
    /// </summary>
    /// <typeparam name="TDbContext">仓储的DbContext</typeparam>
    /// <typeparam name="TAggregateRoot">仓储中的聚合根类型</typeparam>
    public class EfCoreRepository<TDbContext, TAggregateRoot> : BaseRepository<TAggregateRoot>, IEfCoreRepository<TAggregateRoot>
    where TDbContext : IEfCoreDbContext
    where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 当前<see cref="TDbContext"/>的提供者
        /// </summary>
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        private readonly Lazy<XqEntityOptions<TAggregateRoot>> _entityOptionsLazy;

        /// <summary>
        /// 当前仓储的<typeparamref name="TDbContext"/>
        /// </summary>
        protected virtual TDbContext DbContext => _dbContextProvider.GetDbContext();


        #region   构造函数

        public EfCoreRepository(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;

            _entityOptionsLazy = new Lazy<XqEntityOptions<TAggregateRoot>>(
                () => ServiceProvider
                          .GetRequiredService<IOptions<XqEntityOptions>>()
                          .Value
                          .GetOrNull<TAggregateRoot>() ?? XqEntityOptions<TAggregateRoot>.Empty
            );
        }

     

        #endregion

        #region IEfCoreRepository实现
        DbContext IEfCoreRepository<TAggregateRoot>.DbContext => DbContext.As<DbContext>();

        /// <summary>
        /// 获取<typeparamref name="TAggregateRoot"/>仓储，来自<typeparamref name="TDbContext"/>中<typeparamref name="TAggregateRoot"/>仓储定义
        /// </summary>
        public virtual DbSet<TAggregateRoot> DbSet => DbContext.Set<TAggregateRoot>();

        #endregion

        public override async Task<TAggregateRoot> InsertAsync(TAggregateRoot entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }

            return savedEntity;
        }

        public override async Task<TAggregateRoot> UpdateAsync(TAggregateRoot entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }

            return updatedEntity;
        }

        public override async Task DeleteAsync(TAggregateRoot entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
        }

        public override async Task DeleteAsync(Expression<Func<TAggregateRoot, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable()
                .Where(predicate)
                .ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);

            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
        }

        public override async Task<List<TAggregateRoot>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false) 
                : await DbSet.ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
        }

        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.LongCountAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
        }

        protected override IQueryable<TAggregateRoot> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public override IQueryable<TAggregateRoot> WithDetails()
        {
            if (XqEntityOptions.DefaultWithDetailsFunc == null)
            {
                return base.WithDetails();
            }

            return XqEntityOptions.DefaultWithDetailsFunc(GetQueryable());
        }

        public override Task<TAggregateRoot> FindAsync(object id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
