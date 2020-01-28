using Microsoft.Extensions.FileProviders;
using Xqwyf.DependencyInjection;


namespace  Xqwyf.VirtualFileSystem
{
    /// <summary>
    /// 动态文件提供者接口
    /// </summary>
    public interface IDynamicFileProvider : IFileProvider
    {
        /// <summary>
        /// 添加或者修改文件<paramref name="fileInfo"/>
        /// </summary>
        /// <param name="fileInfo"></param>
        void AddOrUpdate(IFileInfo fileInfo);

        /// <summary>
        /// 根据给定的<paramref name="filePath"/>，删除一个文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool Delete(string filePath);
    }
}
