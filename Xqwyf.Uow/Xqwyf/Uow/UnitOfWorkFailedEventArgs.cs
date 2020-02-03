using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Xqwyf.Uow
{
    /// <summary>
    /// 用于<see cref="IUnitOfWork.Failed"/> 的事件参数
    /// </summary>
    public class UnitOfWorkFailedEventArgs : UnitOfWorkEventArgs
    {
        /// <summary>
        /// Exception that caused failure. This is set only if an error occurred during <see cref="IUnitOfWork.Complete"/>.
        /// Can be null if there is no exception, but <see cref="IUnitOfWork.Complete"/> is not called. 
        /// Can be null if another exception occurred during the UOW.
        /// </summary>
        [CanBeNull]
        public Exception Exception { get; }

        /// <summary>
        /// True, if the unit of work is manually rolled back.
        /// </summary>
        public bool IsRolledback { get; }

        /// <summary>
        /// 创建一个 <see cref="UnitOfWorkFailedEventArgs"/> 对象
        /// </summary>
        public UnitOfWorkFailedEventArgs([NotNull] IUnitOfWork unitOfWork, [CanBeNull] Exception exception, bool isRolledback)
            : base(unitOfWork)
        {
            Exception = exception;
            IsRolledback = isRolledback;
        }
    }
}
