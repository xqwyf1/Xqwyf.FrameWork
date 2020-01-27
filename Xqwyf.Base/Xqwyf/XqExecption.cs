using System;
using System.Runtime.Serialization;

namespace Xqwyf
{
    /// <summary>
    /// 所有异常基础类
    /// </summary>
    public class XqException : Exception
    {
        /// <summary>
        /// 创建一个新的<see cref="XqExecption"/>对象
        /// </summary>
        public XqException()
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="XqExecption"/>对象
        /// </summary>
        /// <param name="message">异常消息</param>
        public XqException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// 创建一个新的<see cref="XqExecption"/>对象
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内联异常</param>
        public XqException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        ///  serializing的构造函数
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="context"></param>
        public XqException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
