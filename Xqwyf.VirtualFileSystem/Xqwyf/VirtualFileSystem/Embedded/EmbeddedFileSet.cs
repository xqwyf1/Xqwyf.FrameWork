using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.FileProviders;

namespace  Xqwyf.VirtualFileSystem.Embedded
{
    /// <summary>
    /// 内嵌文件集合
    /// </summary>
    public class EmbeddedFileSet : IVirtualFileSet
    {
        /// <summary>
        /// 文件所在的程序集
        /// </summary>
        [NotNull]
        public Assembly Assembly { get; }

        /// <summary>
        /// 对象的命名空间
        /// </summary>
        [CanBeNull]
        public string BaseNamespace { get; }

        /// <summary>
        /// 文件在工程中的文件夹
        /// </summary>
        [CanBeNull]
        public string BaseFolderInProject { get; }

        /// <summary>
        /// 创建一个<see cref="EmbeddedFileSet"/>对象
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="baseNamespace">命名空间</param>
        /// <param name="baseFolderInProject">文件夹</param>
        public EmbeddedFileSet(
            [NotNull] Assembly assembly,
            [CanBeNull] string baseNamespace = null,
            [CanBeNull] string baseFolderInProject = null)
        {
            XqCheck.NotNull(assembly, nameof(assembly));

            Assembly = assembly;
            BaseNamespace = baseNamespace;
            BaseFolderInProject = baseFolderInProject;
        }

        #region IVirtualFileSet实现

        /// <summary>
        /// 在内嵌文件集合中添加文件
        /// </summary>
        /// <param name="files">被添加的文件</param>
        public void AddFiles(Dictionary<string, IFileInfo> files)
        {
            var lastModificationTime = GetLastModificationTime();

            foreach (var resourcePath in Assembly.GetManifestResourceNames())
            {
                if (!BaseNamespace.IsNullOrEmpty() && !resourcePath.StartsWith(BaseNamespace))
                {
                    continue;
                }

                var fullPath = ConvertToRelativePath(resourcePath).EnsureStartsWith('/');

                if (fullPath.Contains("/"))
                {
                    AddDirectoriesRecursively(files, fullPath.Substring(0, fullPath.LastIndexOf('/')), lastModificationTime);
                }

                files[fullPath] = new EmbeddedResourceFileInfo(
                    Assembly,
                    resourcePath,
                    fullPath,
                    CalculateFileName(fullPath),
                    lastModificationTime
                );
            }
        }

        #endregion

        private static void AddDirectoriesRecursively(Dictionary<string, IFileInfo> files, string directoryPath, DateTimeOffset lastModificationTime)
        {
            if (files.ContainsKey(directoryPath))
            {
                return;
            }

            files[directoryPath] = new VirtualDirectoryFileInfo(
                directoryPath,
                CalculateFileName(directoryPath),
                lastModificationTime
            );

            if (directoryPath.Contains("/"))
            {
                AddDirectoriesRecursively(files, directoryPath.Substring(0, directoryPath.LastIndexOf('/')), lastModificationTime);
            }
        }

        /// <summary>
        /// 获取最后修改日期
        /// </summary>
        /// <returns></returns>
        private DateTimeOffset GetLastModificationTime()
        {
            var lastModified = DateTimeOffset.UtcNow;

            if (!string.IsNullOrEmpty(Assembly.Location))
            {
                try
                {
                    lastModified = File.GetLastWriteTimeUtc(Assembly.Location);
                }
                catch (PathTooLongException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            return lastModified;
        }

        /// <summary>
        /// 转换到相对路径
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private string ConvertToRelativePath(string resourceName)
        {
            if (!BaseNamespace.IsNullOrEmpty())
            {
                resourceName = resourceName.Substring(BaseNamespace.Length + 1);
            }

            var pathParts = resourceName.Split('.');
            if (pathParts.Length <= 2)
            {
                return resourceName;
            }

            var folder = pathParts.Take(pathParts.Length - 2).JoinAsString("/");
            var fileName = pathParts[pathParts.Length - 2] + "." + pathParts[pathParts.Length - 1];

            return folder + "/" + fileName;
        }

        private static string CalculateFileName(string filePath)
        {
            if (!filePath.Contains("/"))
            {
                return filePath;
            }

            return filePath.Substring(filePath.LastIndexOf("/", StringComparison.Ordinal) + 1);
        }
    }
}
