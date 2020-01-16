using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 所有聚合的基础类
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
    }
}
