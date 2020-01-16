using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Repositories;

namespace Xqwyf.Domain.Repositories.EntityFrameworkCore
{
    public class EfCoreRepository<TDbContext, TAggregateRoot> : RepositoryBase<TAggregateRoot>, IEfCoreRepository<TAggregateRoot>
    where TDbContext : IEfCoreDbContext
    where TAggregateRoot : class, IAggregateRoot
    {
    }
}
