using JetBrains.Annotations;

namespace Xqwyf.Uow
{
    /// <summary>
    /// IUnitOfWork的访问器
    /// </summary>
    public interface IUnitOfWorkAccessor
    {
        /// <summary>
        /// 访问器中的<see cref="IUnitOfWork"/>
        /// </summary>
        [CanBeNull]
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 设置访问器中的<see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="unitOfWork"></param>
        void SetUnitOfWork([CanBeNull] IUnitOfWork unitOfWork);
    }
}
