using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Xqwyf.Domain.Repositories
{
    /// <summary>
    /// 只读仓储基础类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadOnlyRepository<TAggregateRoot> : IRepository
       where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 获取所有的聚合根列表
        /// </summary>
        /// <param name="includeDetails">说明是否包括子实体，默认不包括</param>
        /// <param name="cancellationToken">说明操作是否可以取消</param>
        /// <returns></returns>
        Task<List<TAggregateRoot>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取聚合根个数
        /// </summary>
        /// <param name="cancellationToken">说明操作是否可以取消</param>
        /// <returns></returns>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
    }
}
