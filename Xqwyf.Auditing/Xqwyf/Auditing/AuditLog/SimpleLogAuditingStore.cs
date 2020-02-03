using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xqwyf.DependencyInjection;

namespace Xqwyf.Auditing
{
    /// <summary>
    /// 简单的审计内容持久化
    /// </summary>
    [Dependency(TryRegister = true)]
    public class SimpleLogAuditingStore : IAuditingStore, ISingletonDependency
    {
        public ILogger<SimpleLogAuditingStore> Logger { get; set; }

        public SimpleLogAuditingStore()
        {
            Logger = NullLogger<SimpleLogAuditingStore>.Instance;
        }

        public Task SaveAsync(AuditLogInfo auditInfo)
        {
            Logger.LogInformation(auditInfo.ToString());
            return Task.FromResult(0);
        }
    }
}
