using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Auditing
{
    public class AuditLogScope : IAuditLogScope
    {
        public AuditLogInfo Log { get; }

        public AuditLogScope(AuditLogInfo log)
        {
            Log = log;
        }
    }
}
