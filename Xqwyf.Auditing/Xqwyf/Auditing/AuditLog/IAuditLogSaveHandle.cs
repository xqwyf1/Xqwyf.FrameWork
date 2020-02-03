using System;
using System.Threading.Tasks;

namespace Xqwyf.Auditing
{
    public interface IAuditLogSaveHandle : IDisposable
    {
        Task SaveAsync();
    }
}
