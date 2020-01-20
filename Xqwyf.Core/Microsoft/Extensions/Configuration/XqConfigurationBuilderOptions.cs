using System.Reflection;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// 用于应用创建时的Config信息
    /// </summary>
    public class XqConfigurationBuilderOptions
    {
        /// <summary>
        /// 用于设置程序集，该程序集用于获取应用程序的用户密码。
        /// Use this or <see cref="UserSecretsId"/> (higher priority)
        /// </summary>
        public Assembly UserSecretsAssembly { get; set; }

        /// <summary>
        /// Used to set user secret id for the application.
        /// Use this (higher priority) or <see cref="UserSecretsAssembly"/>
        /// </summary>
        public string UserSecretsId { get; set; }

        /// <summary>
        /// 获取应用设置的文件名称，默认是appsettings
        /// </summary>
        public string FileName { get; set; } = "appsettings";

        /// <summary>
        /// 环境名称，通常是 "Development", "Staging" 或者"Production".
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// 配置文件<see cref="FileName"/>.所在的路径
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 用于环境变量的缩写
        /// </summary>
        public string EnvironmentVariablesPrefix { get; set; }

        /// <summary>
        /// 命令行参数
        /// </summary>
        public string[] CommandLineArgs { get; set; }
    }
}
