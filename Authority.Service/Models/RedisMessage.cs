using DotNetty_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authority.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string WebSocketId { get; set; }
        /// <summary>
        /// 发送者消息名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long CreateaTime { get; set; } = DateTime.Now.GetTimeStamp();
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
