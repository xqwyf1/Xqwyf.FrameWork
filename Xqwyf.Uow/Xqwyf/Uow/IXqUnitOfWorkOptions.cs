using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace  Xqwyf.Uow
{
    public interface IXqUnitOfWorkOptions
    {
        bool IsTransactional { get; }

        IsolationLevel? IsolationLevel { get; }

        TimeSpan? Timeout { get; }
    }
}
