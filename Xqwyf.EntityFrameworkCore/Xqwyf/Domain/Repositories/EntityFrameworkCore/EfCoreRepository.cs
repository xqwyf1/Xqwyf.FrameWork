using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Repositories;
using Xqwyf.EntityFrameworkCore;

namespace Xqwyf.Domain.Repositories.EntityFrameworkCore
{
    public class EfCoreRepository<TDbContext, TAggregateRoot> : BaseRepository<TAggregateRoot>, IEfCoreRepository<TAggregateRoot>
    where TDbContext : IEfCoreDbContext
    where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;
        protected virtual TDbContext DbContext => _dbContextProvider.GetDbContext();

        DbContext IEfCoreRepository<TAggregateRoot>.DbContext => DbContext.As<DbContext>();
        public virtual DbSet<TAggregateRoot> DbSet => DbContext.Set<TAggregateRoot>();

        public override async Task<TAggregateRoot> InsertAsync(TAggregateRoot entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }

            return savedEntity;
        }
    }
}
