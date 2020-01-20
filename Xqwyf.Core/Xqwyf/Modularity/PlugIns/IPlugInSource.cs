using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.Annotations;

namespace  Xqwyf.Modularity.PlugIns
{

    /// <summary>
    /// 外部插件源接口，外部插件必须也是XqModule
    /// </summary>
    public interface IPlugInSource
    {
        /// <summary>
        /// 获取插件中所有的Module
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Type[] GetModules();
    }
}
