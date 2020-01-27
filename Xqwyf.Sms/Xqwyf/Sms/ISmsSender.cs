using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace  Xqwyf.Sms
{
    /// <summary>
    /// 短信发送接口
    /// </summary>
    public interface ISmsSender
    {
        Task SendAsync(SmsMessage smsMessage);
    }
}
