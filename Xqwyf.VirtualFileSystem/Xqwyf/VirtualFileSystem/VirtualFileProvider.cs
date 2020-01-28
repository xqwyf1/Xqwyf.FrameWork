using System;
using System.Collections.Generic;
using System.Linq;
using Xqwyf.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Xqwyf.VirtualFileSystem
{
    /// <summary>
    /// 虚拟文件提供者
    /// </summary>
    public class VirtualFileProvider : IVirtualFileProvider, ISingletonDependency
    {
        #region IVirtualFileProvider实现

        /// <summary>
        /// 根据给定的路径，获取一个文件的相关信息
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public virtual IFileInfo GetFileInfo(string subpath)
        {
            return _fileProvider.GetFileInfo(subpath);
        }

        public virtual IChangeToken Watch(string filter)
        {
            return _fileProvider.Watch(filter);
        }

        public virtual IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _fileProvider.GetDirectoryContents(subpath);
        }

        #endregion

        private readonly IFileProvider _fileProvider;
        private readonly XqVirtualFileSystemOptions _options;

        public VirtualFileProvider(
          IOptions<XqVirtualFileSystemOptions> options,
          IDynamicFileProvider dynamicFileProvider)
        {
            _options = options.Value;
            _fileProvider = CreateProvider(dynamicFileProvider);
        }

     

        protected virtual IFileProvider CreateProvider(IDynamicFileProvider dynamicFileProvider)
        {
            var fileProviders = new List<IFileProvider>();

            fileProviders.Add(dynamicFileProvider);

            if (_options.FileSets.PhysicalPaths.Any())
            {
                fileProviders.AddRange(
                    _options.FileSets.PhysicalPaths
                        .Select(rootPath => new PhysicalFileProvider(rootPath))
                        .Reverse()
                );
            }

            fileProviders.Add(new InternalVirtualFileProvider(_options));

            return new CompositeFileProvider(fileProviders);
        }

        protected class InternalVirtualFileProvider : DictionaryBasedFileProvider
        {
            protected override IDictionary<string, IFileInfo> Files => _files.Value;

            private readonly XqVirtualFileSystemOptions _options;
            private readonly Lazy<Dictionary<string, IFileInfo>> _files;

            public InternalVirtualFileProvider(XqVirtualFileSystemOptions options)
            {
                _options = options;
                _files = new Lazy<Dictionary<string, IFileInfo>>(
                    CreateFiles,
                    true
                );
            }

            private Dictionary<string, IFileInfo> CreateFiles()
            {
                var files = new Dictionary<string, IFileInfo>(StringComparer.OrdinalIgnoreCase);

                foreach (var set in _options.FileSets)
                {
                    set.AddFiles(files);
                }

                return files;
            }

            protected override string NormalizePath(string subpath)
            {
                return VirtualFilePathHelper.NormalizePath(subpath);
            }
        }
    }
}
