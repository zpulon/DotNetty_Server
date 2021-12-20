
using DotNetty_Common;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace DotNetty_Server_CoreImpl
{
    public static class MIMEManager
    {
        private static readonly ConcurrentDictionary<string, string> _mimeDic;
        static MIMEManager()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MIMEConfig.json");
            if (!File.Exists(configFilePath)) throw new DotNettyServerException("MIMEConfig.json文件丢失");
            string jsonConfigString = File.ReadAllText(configFilePath);
            _mimeDic = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(jsonConfigString);
        }
        /// <summary>
        /// 获得ContentType
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetContentType(string extension)
        {
            return _mimeDic.ContainsKey(extension) ? _mimeDic[extension] : "application/octet-stream";
        }
    }
}
