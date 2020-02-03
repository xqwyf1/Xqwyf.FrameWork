using System;
using JetBrains.Annotations;

namespace Xqwyf.Uow
{
    /// <summary>
    /// Uow事件参数
    /// </summary>
    public class UnitOfWorkEventArgs : EventArgs
    {
        /// <summary>
        /// 本事件相关的<see cref="IUnitOfWork"/>
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        public UnitOfWorkEventArgs([NotNull] IUnitOfWork unitOfWork)
        {
            XqCheck.NotNull(unitOfWork, nameof(unitOfWork));

            UnitOfWork = unitOfWork;
        }
    }
}
