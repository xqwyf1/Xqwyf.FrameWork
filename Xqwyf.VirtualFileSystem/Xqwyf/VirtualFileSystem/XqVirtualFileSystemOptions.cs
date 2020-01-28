using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf.VirtualFileSystem
{
   /// <summary>
   /// 虚拟文件选项
   /// </summary>
    public class XqVirtualFileSystemOptions
    {
        /// <summary>
        /// 文件集
        /// </summary>
        public VirtualFileSetList FileSets { get; }

        public XqVirtualFileSystemOptions()
        {
            FileSets = new VirtualFileSetList();
        }
    }
}
