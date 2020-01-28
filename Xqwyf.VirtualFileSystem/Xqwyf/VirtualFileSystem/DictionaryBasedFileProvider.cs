using System;
using System.Collections.Generic;
using System.Linq;
using Xqwyf.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using Xqwyf.VirtualFileSystem.Embedded;

namespace Xqwyf.VirtualFileSystem
{
    /// <summary>
    /// 文件字典
    /// </summary>
    public abstract class DictionaryBasedFileProvider : IFileProvider
    {
        protected abstract IDictionary<string, IFileInfo> Files { get; }

        /// <summary>
        /// 根据给定的<paramref name="subpath"/>，获取文件或者目录
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public virtual IFileInfo GetFileInfo(string subpath)
        {
            if (subpath.IsNullOrEmpty())
            {
                return new NotFoundFileInfo(subpath);
            }

            var file = Files.GetOrDefault(NormalizePath(subpath));

            if (file == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            return file;
        }

        /// <summary>
        /// 根据给定<paramref name="subpath"/>，获取该路径下的所有内容
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public virtual IDirectoryContents GetDirectoryContents(string subpath)
        {
            var directory = GetFileInfo(subpath);
            if (!directory.IsDirectory)
            {
                return NotFoundDirectoryContents.Singleton;
            }

            var fileList = new List<IFileInfo>();

            var directoryPath = subpath.EnsureEndsWith('/');
            foreach (var fileInfo in Files.Values)
            {
                var fullPath = fileInfo.GetVirtualOrPhysicalPathOrNull();
                if (!fullPath.StartsWith(directoryPath))
                {
                    continue;
                }

                var relativePath = fullPath.Substring(directoryPath.Length);
                if (relativePath.Contains("/"))
                {
                    continue;
                }

                fileList.Add(fileInfo);
            }

            return new EnumerableDirectoryContents(fileList);
        }

        public virtual IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }

        protected virtual string NormalizePath(string subpath)
        {
            return subpath;
        }
    }
}
