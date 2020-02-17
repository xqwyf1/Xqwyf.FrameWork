using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Application.Dtos
{
    public abstract class EntityDto : IEntityDto //TODO: Consider to delete this class
    {
        public override string ToString()
        {
            return $"[DTO: {GetType().Name}]";
        }
    }
}
