using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Domain.Entities
{
    public interface IHasConcurrencyStamp
    {
        string ConcurrencyStamp { get; set; }
    }
}
