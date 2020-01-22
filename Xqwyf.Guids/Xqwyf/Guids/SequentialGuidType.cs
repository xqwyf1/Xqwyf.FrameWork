using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.Guids
{
    // <summary>
    /// 描述顺序Guid的值类型
    /// </summary>
    public enum SequentialGuidType
    {
        /// <summary>
        /// The GUID should be sequential when formatted using the <see cref="Guid.ToString()" /> method.
        /// Used by MySql and PostgreSql.
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// The GUID should be sequential when formatted using the <see cref="Guid.ToByteArray" /> method.
        /// Used by Oracle.
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// The sequential portion of the GUID should be located at the end of the Data4 block.
        /// Used by SqlServer.
        /// </summary>
        SequentialAtEnd
    }
}
