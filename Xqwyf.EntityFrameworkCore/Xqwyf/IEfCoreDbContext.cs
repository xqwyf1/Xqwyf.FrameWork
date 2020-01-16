using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace Xqwyf.EntityFrameworkCore
{
    public interface IEfCoreDbContext : IDisposable, IInfrastructure<IServiceProvider>, IDbSetCache, IDbContextPoolable
    {
        EntityEntry Attach([NotNull] object entity);

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<T> Set<T>()
            where T : class;

        DatabaseFacade Database { get; }
    }
}
