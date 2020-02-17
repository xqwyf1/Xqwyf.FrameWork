using System.Diagnostics;

namespace  System.Reflection
{
    public static class XqAssemblyExtensions
    {
        /// <summary>
        /// 获取某个文件的版本信息
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetFileVersion(this Assembly assembly)
        {
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }
    }
}
