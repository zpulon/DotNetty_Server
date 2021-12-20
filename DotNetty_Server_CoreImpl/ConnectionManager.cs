using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetty_Server_CoreImpl
{
   public class ConnectionManager
    {
        public static ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext> _connections = new ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classRoomId"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IChannelHandlerContext GetChannelById(string classRoomId, string userId, string name)
        {

            Tuple<string, string, string> tuple = new Tuple<string, string, string>(classRoomId, userId, name);
            var result = _connections.FirstOrDefault(x => x.Key == tuple).Value;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classRoomId"></param>
        /// <returns></returns>
        public ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext> GetAllConnectionByClassRoomId(string classRoomId)
        {
            var dictionary = _connections.Where(x => x.Key.Item1 == classRoomId).ToDictionary(z => z.Key, z => z.Value);
            ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext> concurrentDictionary =
            new ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext>(dictionary);
            return concurrentDictionary;
        }
        public ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext> GetSelfChannel(string classRoomId, string userId, string name)
        {
            var dictionary = _connections.Where(x => x.Key.Item1 == classRoomId && x.Key.Item2 == userId && x.Key.Item3 == name).ToDictionary(z => z.Key, z => z.Value);
            ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext> concurrentDictionary =
            new ConcurrentDictionary<Tuple<string, string, string>, IChannelHandlerContext>(dictionary);
            return concurrentDictionary;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>元组 第一个 教室标识 第二个用户标识</returns>
        public Tuple<string, string, string> GetId(IChannelHandlerContext  channel)
        {
            var key = _connections.FirstOrDefault(x => x.Value == channel).Key;
            Tuple<string, string, string> tuple = new Tuple<string, string, string>(key.Item1, key.Item2, key.Item3);
            return tuple;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classRoomId"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task RemoveChannelHandlerContextAsync(string classRoomId, string userId, string name)
        {
            Tuple<string, string, string> tuple = new Tuple<string, string, string>(classRoomId, userId, name);
            _connections.TryRemove(tuple, out var channel);
        }
        public async Task AddChannelHandlerAsync(IChannelHandlerContext channel, string classRoomId, string userId, string name)
        {
            Tuple<string, string, string> tuple = new Tuple<string, string, string>(classRoomId, userId, name);
            _connections.TryAdd(tuple, channel);
            //await _ichatSessionService.Incr(classRoomId);
        }

    }
}
