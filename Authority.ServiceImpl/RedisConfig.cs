using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
  public  class RedisConfig
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
        private const string DefaultConfigFileName = "redis";
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
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
        }
        #endregion
        #region 配置
        private static RedisServerConfig _serverConfig;
        /// <summary>
        /// 服务配置
        /// </summary>
        public static RedisServerConfig ServerConfig => _serverConfig ?? (_serverConfig = new RedisServerConfig
        {
            Connection = Configuration["Redis:Connection"],
            PassWord = Configuration["Redis:PassWord"],
            InstanceName = Configuration["Redis:InstanceName"],
            DefaultDB = Convert.ToInt32(Configuration["Redis:DefaultDB"])
        });
        #endregion
    }
}
