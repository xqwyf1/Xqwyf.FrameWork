using System.Threading.Tasks;
namespace Xqwyf.Auditing
{
    /// <summary>
    /// 审计内容持久化接口
    /// </summary>
    public interface IAuditingStore
    {
        Task SaveAsync(AuditLogInfo auditInfo);
    }
}
