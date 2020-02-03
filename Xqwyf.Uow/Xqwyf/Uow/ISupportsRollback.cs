using System.Threading;
using System.Threading.Tasks;

namespace Xqwyf.Uow
{
    /// <summary>
    /// 支持RollBack的接口
    /// </summary>
    public interface ISupportsRollback
    {
        /// <summary>
        /// <see cref="ISupportsRollback"/>回滚操作
        /// </summary>
        void Rollback();

        /// <summary>
        ///  <see cref="ISupportsRollback"/>异步回滚操作
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RollbackAsync(CancellationToken cancellationToken);
    }
}
