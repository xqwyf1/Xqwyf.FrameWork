using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Xqwyf.Domain.Entities;
using Xqwyf.Domain.Repositories;

namespace Xqwyf.Domain.Repositories.EntityFrameworkCore
{
    public interface IEfCoreRepository<TAggregateRoot> : IBasicRepository<TAggregateRoot>
     where TAggregateRoot : class, IAggregateRoot
    {
        DbContext DbContext { get; }

        DbSet<TAggregateRoot> DbSet { get; }
    }
}
