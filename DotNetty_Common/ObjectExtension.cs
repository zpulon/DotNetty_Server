
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DotNetty_Common
{
    public static class ObjectExtension
    {
        private static readonly ConcurrentDictionary<Type, Func<object, object>> ConvertDictionary = new ConcurrentDictionary<Type, Func<object, object>>();
        static ObjectExtension()
        {
            ConvertDictionary.TryAdd(typeof(bool), WrapValueConvert(Convert.ToBoolean));
            ConvertDictionary.TryAdd(typeof(bool?), WrapValueConvert(Convert.ToBoolean));
            ConvertDictionary.TryAdd(typeof(int), WrapValueConvert(Convert.ToInt32));
            ConvertDictionary.TryAdd(typeof(int?), WrapValueConvert(Convert.ToInt32));
            ConvertDictionary.TryAdd(typeof(long), WrapValueConvert(Convert.ToInt64));
            ConvertDictionary.TryAdd(typeof(long?), WrapValueConvert(Convert.ToInt64));
            ConvertDictionary.TryAdd(typeof(short), WrapValueConvert(Convert.ToInt16));
            ConvertDictionary.TryAdd(typeof(short?), WrapValueConvert(Convert.ToInt16));
            ConvertDictionary.TryAdd(typeof(double), WrapValueConvert(Convert.ToDouble));
            ConvertDictionary.TryAdd(typeof(double?), WrapValueConvert(Convert.ToDouble));
            ConvertDictionary.TryAdd(typeof(float), WrapValueConvert(Convert.ToSingle));
            ConvertDictionary.TryAdd(typeof(float?), WrapValueConvert(Convert.ToSingle));
            ConvertDictionary.TryAdd(typeof(Guid), m => Guid.Parse(m.ToString()) as object);
            ConvertDictionary.TryAdd(typeof(Guid?), m => Guid.Parse(m.ToString()) as object);
            ConvertDictionary.TryAdd(typeof(string), Convert.ToString);
            ConvertDictionary.TryAdd(typeof(DateTime), WrapValueConvert(Convert.ToDateTime));
            ConvertDictionary.TryAdd(typeof(DateTime?), WrapValueConvert(Convert.ToDateTime));
        }
        /// <summary>
        /// Object转换为Json
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static string ToJson(this object inputObj)
        {
            return JsonConvert.SerializeObject(inputObj);
        }
        /// <summary>
        /// 转换为
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object ConvertTo(this object inputObj, Type targetType)
        {
            if (inputObj == null)
            {
                if (targetType.IsValueType) throw new DotNettyServerException($"不能将null转换为{targetType.Name}");
                return null;
            }
            if (inputObj.GetType() == targetType || targetType.IsInstanceOfType(inputObj))
            {
                return inputObj;
            }
            if (ConvertDictionary.ContainsKey(targetType))
            {
                return ConvertDictionary[targetType](inputObj);
            }
            try
            {
                return Convert.ChangeType(inputObj, targetType);
            }
            catch (Exception ex)
            {
                throw new DotNettyServerException($"未实现到{targetType.Name}的转换方法", ex);
            }
        }
        #region 私有方法
        /// <summary>
        /// 包装值转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        private static Func<object, object> WrapValueConvert<T>(Func<object, T> input) where T : struct
        {
            return i =>
            {
                if (i == null || i is DBNull) return null;
                return input(i);
            };
        }
        #endregion
    }
}
