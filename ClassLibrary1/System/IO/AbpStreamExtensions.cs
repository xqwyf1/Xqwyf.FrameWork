using System.Threading.Tasks;

namespace System.IO
{
    public static class AbpStreamExtensions
    {
        /// <summary>
        /// 读取<paramref name="stream"/>的所有内容
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 读取<paramref name="stream"/>的所有内容
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>

        public static async Task<byte[]> GetAllBytesAsync(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
                return memoryStream.ToArray();
            }
        }
    }
}