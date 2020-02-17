using System.Threading;
using System.Threading.Tasks;
using Xqwyf.EntityFrameworkCore;

namespace Xqwyf.Uow.EntityFrameworkCore
{
    /// <summary>
    /// 具有Ef的DataBase
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class EfCoreDatabaseApi<TDbContext> : IDatabaseApi, ISupportsSavingChanges
          where TDbContext : IEfCoreDbContext
    {
        public TDbContext DbContext { get; }

        public EfCoreDatabaseApi(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
