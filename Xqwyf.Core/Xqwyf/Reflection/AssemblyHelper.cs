using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Xqwyf.Reflection
{
    /// <summary>
    /// 程序集帮助类
    /// </summary>
    internal static class AssemblyHelper
    {
        /// <summary>
        /// 根据文件夹位置和搜索选项，获取所有的程序集
        /// </summary>
        /// <param name="folderPath">程序集所在位置</param>
        /// <param name="searchOption">搜索选项，分为当前文件夹和包括所有文件夹</param>
        /// <returns>获取的程序集</returns>
        public static List<Assembly> LoadAssemblies(string folderPath, SearchOption searchOption)
        {
            return GetAssemblyFiles(folderPath, searchOption)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToList();
        }


        /// <summary>
        /// 根据文件夹位置和搜索选项，获取所有的程序集文件全路径
        /// </summary>
        /// <param name="folderPath">程序集所在位置</param>
        /// <param name="searchOption">搜索选项，分为当前文件夹和包括所有文件夹</param>
        /// <returns>程序集路径列表</returns>
        public static IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption)
        {
            return Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
        }

        /// <summary>
        /// 获取某个程序集中的所有类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>类型列表</returns>
        public static IReadOnlyList<Type> GetAllTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }
    }
}
