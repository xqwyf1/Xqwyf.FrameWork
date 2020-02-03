using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace  Xqwyf.Uow
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDatabaseApiContainer, ITransactionApiContainer, IDisposable
    {
        /// <summary>
        /// Uow的Id
        /// </summary>
        Guid Id { get; }

        //TODO: Switch to OnFailed (sync) and OnDisposed (sync) methods to be compatible with OnCompleted
       /// <summary>
       /// Uow进行处理时的失败事件
       /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// Uow销毁事件
        /// </summary>
        event EventHandler<UnitOfWorkEventArgs> Disposed;

        /// <summary>
        /// 获取Uow的Option
        /// </summary>
        IXqUnitOfWorkOptions Options { get; }


        /// <summary>
        /// 获取外部的<see cref="IUnitOfWork"/>
        /// </summary>
        IUnitOfWork Outer { get; }

        /// <summary>
        /// 预留？
        /// </summary>
        bool IsReserved { get; }


        /// <summary>
        /// 是否已经释放
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 是否已经完成
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 预留的名称
        /// </summary>
        string ReservationName { get; }

        /// <summary>
        /// 设置外部的<paramref name="outer"/>
        /// </summary>
        /// <param name="outer"></param>
        void SetOuter([CanBeNull] IUnitOfWork outer);


        /// <summary>
        /// 通过<paramref name="options"/>初始化
        /// </summary>
        /// <param name="options"></param>
        void Initialize([NotNull] XqUnitOfWorkOptions options);

        void Reserve([NotNull] string reservationName);

        /// <summary>
        /// 向数据库提交变更
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task CompleteAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);

        void OnCompleted(Func<Task> handler);

        bool IsReservedFor(string reservationName);
       
    }
}
