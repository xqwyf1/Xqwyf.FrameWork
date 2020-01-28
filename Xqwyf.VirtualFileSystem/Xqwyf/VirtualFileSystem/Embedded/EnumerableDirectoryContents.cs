using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Extensions.FileProviders;

namespace  Xqwyf.VirtualFileSystem.Embedded
{
    /// <summary>
    /// 嵌入式目录内容
    /// </summary>
    internal class EnumerableDirectoryContents : IDirectoryContents
    {
        private readonly IEnumerable<IFileInfo> _entries;

        
        public EnumerableDirectoryContents([NotNull] IEnumerable<IFileInfo> entries)
        {
            XqCheck.NotNull(entries, nameof(entries));

            _entries = entries;
        }

        public bool Exists => true;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entries.GetEnumerator();
        }
    }
}
