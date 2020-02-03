using System;
using System.Threading.Tasks;

namespace Xqwyf.Uow 
{
    /// <summary>
    /// 事务接口，表示一个事务
    /// </summary>
    public interface ITransactionApi : IDisposable
    {
        /// <summary>
        /// <see cref="ITransactionApi"/>接口中的提交操作
        /// </summary>
        void Commit();

        /// <summary>
        ///<see cref="ITransactionApi"/>接口中的异步提交操作
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}
