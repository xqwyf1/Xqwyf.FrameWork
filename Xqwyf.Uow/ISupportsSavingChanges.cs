using System.Threading;
using System.Threading.Tasks;


namespace Xqwyf.Uow
{
    public interface ISupportsSavingChanges
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
