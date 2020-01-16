using System.Threading;
using System.Threading.Tasks;


namespace Xqwyf.Uow
{
    /// <summary>
    /// 用于数据连接，定义保存操作
    /// </summary>
    public interface ISupportsSavingChanges
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
