using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace  Xqwyf.Sms
{
    /// <summary>
    /// 记录短信消息内容
    /// </summary>
    public class SmsMessage
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// 短信 内容
        /// </summary>
        public string Text { get; }

        public IDictionary<string, object> Properties { get; }

        /// <summary>
        /// 创建一个短信信息
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="text"></param>
        public SmsMessage([NotNull] string phoneNumber, [NotNull] string text)
        {
            PhoneNumber = XqCheck.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber));
            Text = XqCheck.NotNullOrWhiteSpace(text, nameof(text));

            Properties = new Dictionary<string, object>();
        }
    }
}
