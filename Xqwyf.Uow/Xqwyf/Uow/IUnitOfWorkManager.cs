using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace  Xqwyf.Uow
{
    /// <summary>
    /// Uow管理器接口
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取当前的Uow
        /// </summary>
        [CanBeNull]
        IUnitOfWork Current { get; }

        [NotNull]
        IUnitOfWork Begin([NotNull] XqUnitOfWorkOptions options, bool requiresNew = false);

        [NotNull]
        IUnitOfWork Reserve([NotNull] string reservationName, bool requiresNew = false);

        void BeginReserved([NotNull] string reservationName, [NotNull] XqUnitOfWorkOptions options);

        bool TryBeginReserved([NotNull] string reservationName, [NotNull] XqUnitOfWorkOptions options);
    }
}
