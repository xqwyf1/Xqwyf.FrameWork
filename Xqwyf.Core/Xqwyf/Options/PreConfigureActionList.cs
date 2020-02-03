using System;
using System.Collections.Generic;

namespace  Xqwyf.Options
{
    /// <summary>
    /// 预配置中的方法列表
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public class PreConfigureActionList<TOptions> : List<Action<TOptions>>
    {
        /// <summary>
        /// 通过<typeparamref name="TOptions"/>,为预配置中的方法列表设置<paramref name="options"/>
        /// </summary>
        /// <param name="options"></param>
        public void Configure(TOptions options)
        {
            foreach (var action in this)
            {
                action(options);
            }
        }
    }
}
