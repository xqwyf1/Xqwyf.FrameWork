using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace  Xqwyf.Uow
{
    /// <summary>
    /// 事务容器接口
    /// </summary>
    public interface ITransactionApiContainer
    {
        /// <summary>
        /// 根据<paramref name="key"/>查找一个事务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [CanBeNull]
        ITransactionApi FindTransactionApi([NotNull] string key);

        /// <summary>
        /// 根据<paramref name="key"/>和<paramref name="api"/>添加一个事务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="api"></param>
        void AddTransactionApi([NotNull] string key, [NotNull] ITransactionApi api);

        /// <summary>
        /// 根据<paramref name="key"/>查找一个事务，如果没有，通过<paramref name="factory"/>创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        [NotNull]
        ITransactionApi GetOrAddTransactionApi([NotNull] string key, [NotNull] Func<ITransactionApi> factory);
    }
}
