using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Xqwyf.Auditing
{
    /// <summary>
    /// 审计管理器
    /// </summary>
    public interface IAuditingManager
    {
        [CanBeNull]
        IAuditLogScope Current { get; }

        IAuditLogSaveHandle BeginScope();
    }
}
