using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace  Xqwyf.VirtualFileSystem.Embedded
{
    /// <summary>
    /// 嵌入文件资源的信息
    /// </summary>
    public class EmbeddedResourceFileInfo : IFileInfo
    {
        #region IFileInfo实现

        /// <summary>
        /// 在该存储中，文件是否存在，在此永远存在
        /// </summary>
        public bool Exists => true;

        /// <summary>
        /// 文件的字节长度
        /// </summary>
        public long Length
        {
            get
            {
                if (!_length.HasValue)
                {
                    using (var stream = _assembly.GetManifestResourceStream(_resourcePath))
                    {
                        _length = stream.Length;
                    }
                }

                return _length.Value;
            }
        }

        private long? _length;

        /// <summary>
        /// 文件物理路径，为空
        /// </summary>
        public string PhysicalPath => null;

        /// <summary>
        /// 不包括路径的文件名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 文件的虚拟路径
        /// </summary>
        public string VirtualPath { get; }

        /// <summary>
        /// 文件最后修改时间
        /// </summary>
        public DateTimeOffset LastModified { get; }

        /// <summary>
        /// 文件是否为目录，为False
        /// </summary>
        public bool IsDirectory => false;

        /// <summary>
        /// 在<see cref="EmbeddedResourceFileInfo._resourcePath"/>读取文件
        /// </summary>
        /// <returns></returns>
        public Stream CreateReadStream()
        {
            var stream = _assembly.GetManifestResourceStream(_resourcePath);

            if (!_length.HasValue && stream != null)
            {
                _length = stream.Length;
            }

            return stream;
        }

        #endregion

        private readonly Assembly _assembly;

        private readonly string _resourcePath;


        /// <summary>
        /// 创建一个<see cref="EmbeddedResourceFileInfo"/>对象
        /// </summary>
        /// <param name="assembly">文件所在程序集</param>
        /// <param name="resourcePath">文件的资源路径</param>
        /// <param name="virtualPath">文件的虚拟路径</param>
        /// <param name="name">文件名称</param>
        /// <param name="lastModified">文件的最后修改日期</param>
        public EmbeddedResourceFileInfo(
          Assembly assembly,
          string resourcePath,
          string virtualPath,
          string name,
          DateTimeOffset lastModified)
        {
            _assembly = assembly;
            _resourcePath = resourcePath;

            VirtualPath = virtualPath;
            Name = name;
            LastModified = lastModified;
        }
    }
}
