
using DotNetty_ControllerBus.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using DotNetty_ControllerBus.Filters;
using DotNetty_Common;
using System.Collections.Concurrent;

namespace DotNetty_ControllerBus
{
    public class ControllerHelper
    {
        /// <summary>
        /// 控制器类型字典
        /// </summary>
        private readonly ConcurrentDictionary<string, Type> _controller = new ConcurrentDictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        private readonly List<IFilter> _filters = new List<IFilter>();
        /// <summary>
        /// 添加控制器类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool TryAddController(Type type)
        {
            if (type == null) throw new DotNettyServerException("控制器类型为空");
            if (!type.IsSubclassOf(typeof(BaseController))) throw new DotNettyServerException($"控制器必须继承类{nameof(BaseController)}");
            string key = type.Name;
            var routeAttribute = type.GetCustomAttribute<RouteAttribute>();
            if(routeAttribute != null)
            {
                key = routeAttribute.Key;
            }
            else if (key.EndsWith("Controller"))
            {
                key = key.Substring(0, key.Length - 10);
            }
            if (_controller.ContainsKey(key)) return false;
            
            return _controller.TryAdd(key, type);
        }
        /// <summary>
        /// 获得控制器类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Type GetController(string key)
        {
            if(!_controller.ContainsKey(key)) throw new DotNettyServerException("未找到对应控制器");
            return _controller[key];
        }
        /// <summary>
        /// 添加过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddFilter<T>() where T : IFilter
        {
            Type tType = typeof(T);
            var filter = Activator.CreateInstance(tType) as IFilter;
            AddFilter(filter);
        }
        /// <summary>
        /// 添加过滤器
        /// </summary>
        /// <param name="filter"></param>
        public void AddFilter(IFilter filter)
        {
            _filters.Add(filter);
        }
        /// <summary>
        /// 获得所有过滤器
        /// </summary>
        /// <returns></returns>
        public IFilter[] GetAllFilters()
        {
            return _filters.ToArray();
        }
    }
}
