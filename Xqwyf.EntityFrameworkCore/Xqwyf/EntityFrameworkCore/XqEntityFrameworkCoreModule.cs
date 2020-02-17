using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Xqwyf.Modularity;

using Xqwyf.Uow.EntityFrameworkCore;

namespace  Xqwyf.EntityFrameworkCore
{
    public class XqEntityFrameworkCoreModule : XqModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<XqDbContextOptions>(options =>
            {
                options.PreConfigure(xqDbContextConfigurationContext =>
                {
                    xqDbContextConfigurationContext.DbContextOptions
                        .ConfigureWarnings(warnings =>
                        {
                            warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
                        });
                });
            });

            context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
        }
    }
}
