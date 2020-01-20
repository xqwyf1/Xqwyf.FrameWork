using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using JetBrains.Annotations;

using Xqwyf.Reflection;

namespace Xqwyf.Modularity.PlugIns
{
    /// <summary>
    ///外部文件夹中的模块加载
    /// </summary>
    public class FolderPlugInSource : IPlugInSource
    {
        /// <summary>
        /// 外部插件所在的文件夹
        /// </summary>
        public string Folder { get; }

        /// <summary>
        /// 文件夹的搜索选项
        /// </summary>
        public SearchOption SearchOption { get; set; }

        public Func<string, bool> Filter { get; set; }

        public FolderPlugInSource(
            [NotNull] string folder,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Check.NotNull(folder, nameof(folder));

            Folder = folder;
            SearchOption = searchOption;
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (XqModule.IsXqModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new XqException("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules.ToArray();
        }

        /// <summary>
        /// 获取所有的程序集
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblyFiles = AssemblyHelper.GetAssemblyFiles(Folder, SearchOption);

            if (Filter != null)
            {
                assemblyFiles = assemblyFiles.Where(Filter);
            }

            return assemblyFiles.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
        }
    }
}
