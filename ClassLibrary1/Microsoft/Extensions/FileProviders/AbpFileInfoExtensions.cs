using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Xqwyf;

namespace Microsoft.Extensions.FileProviders
{
    public static class AbpFileInfoExtensions
    {
        /// <summary>
        /// 使用<see cref="Encoding.UTF8"/>读取文件内容  
        /// </summary>
        public static string ReadAsString([NotNull] this IFileInfo fileInfo)
        {
            return fileInfo.ReadAsString(Encoding.UTF8);
        }

        /// <summary>
        /// 使用给定的<paramref name="encoding"/>.读取文件内容  ，并且按照<see cref="String"/>返回
        /// </summary>
        public static string ReadAsString([NotNull] this IFileInfo fileInfo, Encoding encoding)
        {
            XqCheck.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                using (var streamReader = new StreamReader(stream, encoding, true))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 读取文件内容，并且按照<see cref="Byte"/>返回
        /// </summary>
        public static byte[] ReadBytes([NotNull] this IFileInfo fileInfo)
        {
            XqCheck.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                return stream.GetAllBytes();
            }
        }

        /// <summary>
        /// 读取文件内容，并且按照<see cref="Byte"/>返回
        /// </summary>
        public static async Task<byte[]> ReadBytesAsync([NotNull] this IFileInfo fileInfo)
        {
            XqCheck.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                return await stream.GetAllBytesAsync().ConfigureAwait(false);
            }
        }

    }
}
