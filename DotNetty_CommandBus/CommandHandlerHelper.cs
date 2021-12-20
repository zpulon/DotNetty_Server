
using DotNetty_Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotNetty_CommandBus
{
    public class CommandHandlerHelper
    {
        /// <summary>
        /// 命令处理器类型字典
        /// </summary>
        private readonly ConcurrentDictionary<string, Type> _commandHandlers = new ConcurrentDictionary<string, Type>();
        /// <summary>
        /// 添加名命令处理器类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool TryAddCommandHandler(Type type)
        {
            if (type == null) throw new DotNettyServerException("名流处理器类型为空");
            string key = type.Name;
            if (_commandHandlers.ContainsKey(key)) return false;
            _commandHandlers.TryAdd(key, type);
            return true;
        }
        /// <summary>
        /// 获得命令处理器类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Type GetCommandHandler(string key)
        {
            if (!_commandHandlers.ContainsKey(key)) throw new DotNettyServerException("未找到对呀命令处理器");
            return _commandHandlers[key];
        }
    }
}
