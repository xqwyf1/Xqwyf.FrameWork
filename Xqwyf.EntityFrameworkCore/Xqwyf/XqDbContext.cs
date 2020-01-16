using System;

using Microsoft.EntityFrameworkCore;

namespace Xqwyf.EntityFrameworkCore
{
    public abstract class XqDbContext<TDbContext> : DbContext, IEfCoreDbContext
         where TDbContext : DbContext
    {
    }
}
