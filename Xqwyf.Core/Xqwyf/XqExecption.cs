using System;
using System.Runtime.Serialization;

namespace Xqwyf.Core
{
    /// <summary>
    /// 所有异常基础类
    /// </summary>
    public class XqExecption : Exception
    {
        /// <summary>
        /// 创建一个新的<see cref="XqExecption"/>对象
        /// </summary>
        public XqExecption()
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="XqExecption"/>对象
        /// </summary>
        /// <param name="message">异常消息</param>
        public XqExecption(string message)
            : base(message)
        {

        }


        /// <summary>
        /// 创建一个新的<see cref="XqExecption"/>对象
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内联异常</param>
        public XqExecption(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        ///  serializing的构造函数
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="context"></param>
        public XqExecption(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
