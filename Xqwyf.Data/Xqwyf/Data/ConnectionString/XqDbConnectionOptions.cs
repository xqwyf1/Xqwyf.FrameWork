using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Data
{
    /// <summary>
    /// 连接串配置选项
    /// </summary>
    public class XqDbConnectionOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public XqDbConnectionOptions()
        {
            ConnectionStrings = new ConnectionStrings();
        }
    }
}
