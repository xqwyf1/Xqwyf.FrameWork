using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.EntityFrameworkCore
{
    /// <summary>
    /// TDbContext的提供者，TDbContext必须是<see cref="IEfCoreDbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
    where TDbContext : IEfCoreDbContext
    {
        TDbContext GetDbContext();
    }
}
