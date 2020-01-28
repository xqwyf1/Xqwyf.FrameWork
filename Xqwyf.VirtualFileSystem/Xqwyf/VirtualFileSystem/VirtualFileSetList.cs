using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.VirtualFileSystem
{
    /// <summary>
    /// 虚拟文件集合列表
    /// </summary>
    public class VirtualFileSetList : List<IVirtualFileSet>
    {
        /// <summary>
        /// 虚拟文件集合物理路径
        /// </summary>
        public List<string> PhysicalPaths { get; }

        public VirtualFileSetList()
        {
            PhysicalPaths = new List<string>();
        }
    }
}
