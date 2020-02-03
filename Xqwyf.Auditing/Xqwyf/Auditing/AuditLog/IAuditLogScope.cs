
using JetBrains.Annotations;

namespace Xqwyf.Auditing
{
    public interface IAuditLogScope
    {
        [NotNull]
        AuditLogInfo Log { get; }
    }
}
