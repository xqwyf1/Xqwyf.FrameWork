using System.Threading;
using System.Threading.Tasks;


namespace Xqwyf.Uow
{
    /// <summary>
    /// 定义保存操作
    /// </summary>
    public interface ISupportsSavingChanges
    {
        /// <summary>
        /// <see cref="ISupportsSavingChanges"/>中的SaveChanges
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// <see cref="ISupportsSavingChanges"/>中的SaveChanges
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
