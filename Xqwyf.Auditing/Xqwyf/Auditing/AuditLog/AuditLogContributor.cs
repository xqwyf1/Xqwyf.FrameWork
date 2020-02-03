using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Auditing
{
    public abstract class AuditLogContributor
    {
        public virtual void PreContribute(AuditLogContributionContext context)
        {

        }

        public virtual void PostContribute(AuditLogContributionContext context)
        {

        }
    }
}
