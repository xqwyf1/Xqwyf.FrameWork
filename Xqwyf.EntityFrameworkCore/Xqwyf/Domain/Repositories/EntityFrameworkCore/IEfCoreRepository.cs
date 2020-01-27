using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Repositories;

namespace Xqwyf.Domain.Repositories.EntityFrameworkCore
{
    /// <summary>
    /// 基于Ef的Repository的接口，包括当前<see cref="DbContext"/>和相关<typeparamref name="TAggregateRoot"/>的仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IEfCoreRepository<TAggregateRoot> : IBasicRepository<TAggregateRoot>
     where TAggregateRoot : class, IAggregateRoot
    {
        DbContext DbContext { get; }

        DbSet<TAggregateRoot> DbSet { get; }
    }
}
