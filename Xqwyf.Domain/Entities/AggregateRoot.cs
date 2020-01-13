using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 聚合基础类
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
    }
}
