using System;

using JetBrains.Annotations;

using Xqwyf.DependencyInjection;

namespace Xqwyf.Uow
{
    /// <summary>
    /// 数据库容器接口
    /// </summary>
    public interface IDatabaseApiContainer : IServiceProviderAccessor
    {
        /// <summary>
        /// 根据<paramref name="key"/>查找一个数据库
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [CanBeNull]
        IDatabaseApi FindDatabaseApi([NotNull] string key);

        /// <summary>
        /// 根据<paramref name="key"/>和<paramref name="api"/>添加一个数据库
        /// </summary>
        /// <param name="key"></param>
        /// <param name="api"></param>
        void AddDatabaseApi([NotNull] string key, [NotNull] IDatabaseApi api);

        /// <summary>
        /// 根据<paramref name="key"/>获取数据库，如果没有，通过<paramref name="factory"/>创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        [NotNull]
        IDatabaseApi GetOrAddDatabaseApi([NotNull] string key, [NotNull] Func<IDatabaseApi> factory);
    }
}
