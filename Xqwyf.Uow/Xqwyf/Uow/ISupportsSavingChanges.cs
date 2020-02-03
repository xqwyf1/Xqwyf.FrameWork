using System.Threading;
using System.Threading.Tasks;


namespace Xqwyf.Uow
{
    /// <summary>
    /// 定义保存操作
    /// </summary>
    public interface ISupportsSavingChanges
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
