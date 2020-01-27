using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

using Xqwyf.Domain.Entities.Events;

namespace Xqwyf.EntityFrameworkCore
{
    /// <summary>
    /// Ef的DbContext接口
    /// </summary>
    public interface IEfCoreDbContext : IDisposable, IInfrastructure<IServiceProvider>, IDbSetCache, IDbContextPoolable
    {
      

        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        DbSet<T> Set<T>()
          where T : class;

        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }


        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

        EntityEntry Entry([NotNull] object entity);

        #region Attach

        /// <summary>
        /// 追加实体<paramref name="entity"/>
        /// </summary>
        /// <param name="entity">被追加的实体</param>
        /// <returns></returns>
        EntityEntry Attach([NotNull] object entity);

        /// <summary>
        /// 追加实体<paramref name="entity"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity) where TEntity : class;

       

        void AttachRange([NotNull] IEnumerable<object> entities);

        void AttachRange([NotNull] params object[] entities);

        #endregion

        #region SaveChanges

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Add

        EntityEntry Add([NotNull] object entity);

        EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

        ValueTask<EntityEntry> AddAsync([NotNull] object entity, CancellationToken cancellationToken = default);

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNull] TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        void AddRange([NotNull] IEnumerable<object> entities);

        void AddRange([NotNull] params object[] entities);

        Task AddRangeAsync([NotNull] params object[] entities);

        Task AddRangeAsync([NotNull] IEnumerable<object> entities, CancellationToken cancellationToken = default);


        #endregion

        #region Find

        

        object Find([NotNull] Type entityType, [NotNull] params object[] keyValues);

        TEntity Find<TEntity>([NotNull] params object[] keyValues) where TEntity : class;

        ValueTask<object> FindAsync([NotNull] Type entityType, [NotNull] object[] keyValues, CancellationToken cancellationToken);

        ValueTask<TEntity> FindAsync<TEntity>([NotNull] object[] keyValues, CancellationToken cancellationToken) where TEntity : class;

        ValueTask<TEntity> FindAsync<TEntity>([NotNull] params object[] keyValues) where TEntity : class;

        ValueTask<object> FindAsync([NotNull] Type entityType, [NotNull] params object[] keyValues);

        #endregion

        #region Remove

        EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity) where TEntity : class;

        EntityEntry Remove([NotNull] object entity);

        void RemoveRange([NotNull] IEnumerable<object> entities);

        void RemoveRange([NotNull] params object[] entities);


        #endregion

        #region Update

        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

        EntityEntry Update([NotNull] object entity);

        void UpdateRange([NotNull] params object[] entities);

        void UpdateRange([NotNull] IEnumerable<object> entities);
        #endregion

    }
}
