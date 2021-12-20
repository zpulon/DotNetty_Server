
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace DotNetty_Common
{
    public  class ApplicationConfig
    {
        #region 配置对象
       

        private static IConfiguration _configuration;
        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    ConfigurationBuilder();
                }
                return _configuration;
            }
        }
        private const string DefaultConfigFileName = "appsetting";
        private const string DefaultConfigFileSuffix = "json";
        /// <summary>
        /// 配置生成
        /// </summary>
        /// <param name="targetConfig"></param>
        private static void ConfigurationBuilder(string targetConfig = null)
        {
            string appConfigFile = string.IsNullOrEmpty(targetConfig) ?
                $"{DefaultConfigFileName}.{DefaultConfigFileSuffix}" :
                $"{DefaultConfigFileName}.{targetConfig}.{DefaultConfigFileSuffix}";
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile,true,true);
            _configuration = builder.Build();


        }
        #endregion
        #region 配置
        private static List<ServerConfig> _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static List<ServerConfig> ServerConfig => _serverConfig ?? GetConfig();

        private static List<ServerConfig> GetConfig()
        {
            var model = new ServiceCollection().AddOptions().Configure<List<ServerConfig>>(Configuration.GetSection("ServerConfig")).BuildServiceProvider();
            var serverConfigs = model.GetService<IOptions<List<ServerConfig>>>().Value;
            return serverConfigs;
        }
        #endregion
    }
}
