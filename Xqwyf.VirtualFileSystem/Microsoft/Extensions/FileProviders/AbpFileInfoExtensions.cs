using JetBrains.Annotations;
using System.IO;
using System.Text;

using Xqwyf;
using Xqwyf.VirtualFileSystem;
using Xqwyf.VirtualFileSystem.Embedded;

namespace Microsoft.Extensions.FileProviders
{
    public static class AbpFileInfoExtensions
    {
        /// <summary>
        /// 获取虚拟文件或者物理文件的路径
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string GetVirtualOrPhysicalPathOrNull([NotNull] this IFileInfo fileInfo)
        {
            XqCheck.NotNull(fileInfo, nameof(fileInfo));

            if (fileInfo is EmbeddedResourceFileInfo embeddedFileInfo)
            {
                return embeddedFileInfo.VirtualPath;
            }

            if (fileInfo is InMemoryFileInfo inMemoryFileInfo)
            {
                return inMemoryFileInfo.DynamicPath;
            }

            return fileInfo.PhysicalPath;
        }

    }
}
