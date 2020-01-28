using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace  Xqwyf.VirtualFileSystem
{
    /// <summary>
    /// 虚拟文件集合接口
    /// </summary>
    public interface IVirtualFileSet
    {
        /// <summary>
        /// 在文件集合中增加一个文件
        /// </summary>
        /// <param name="files"></param>
        void AddFiles(Dictionary<string, IFileInfo> files);
    }
}
