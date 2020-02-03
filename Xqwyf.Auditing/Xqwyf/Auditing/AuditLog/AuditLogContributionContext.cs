using System;
using System.Collections.Generic;
using System.Text;
using Xqwyf.DependencyInjection;

namespace Xqwyf.Auditing
{
    public class AuditLogContributionContext : IServiceProviderAccessor
    {
        public IServiceProvider ServiceProvider { get; }

        public AuditLogInfo AuditInfo { get; }

        public AuditLogContributionContext(IServiceProvider serviceProvider, AuditLogInfo auditInfo)
        {
            ServiceProvider = serviceProvider;
            AuditInfo = auditInfo;
        }
    }
}
