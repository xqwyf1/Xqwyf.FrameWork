using System;
using System.Collections.Generic;
using System.Text;

using Xqwyf.Auditing;

namespace Xqwyf.Application.Dtos
{
    public abstract class AuditedEntityWithUserDto<TUserDto> : AuditedEntityDto, IAuditedObject<TUserDto>
    {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }
    }
}
