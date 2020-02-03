using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.Data;

namespace Xqwyf.Auditing
{
    [Serializable]
    public class AuditLogActionInfo : IHasExtraProperties
    {
        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string Parameters { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        public Dictionary<string, object> ExtraProperties { get; }

        public AuditLogActionInfo()
        {
            ExtraProperties = new Dictionary<string, object>();
        }
    }
}
