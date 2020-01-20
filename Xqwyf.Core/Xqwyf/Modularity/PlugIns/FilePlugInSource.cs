using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Loader;

namespace Xqwyf.Modularity.PlugIns
{
    /// <summary>
    /// 外部插件对象来源为文件
    /// </summary>
    public class FilePlugInSource : IPlugInSource
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string[] FilePaths { get; }

        /// <summary>
        /// 创建一个<see cref="FilePlugInSource"/>,文件路径为<paramref name="filePaths"/>
        /// </summary>
        public FilePlugInSource(params string[] filePaths)
        {
            FilePaths = filePaths ?? new string[0];
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var filePath in FilePaths)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);

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
    }
}
