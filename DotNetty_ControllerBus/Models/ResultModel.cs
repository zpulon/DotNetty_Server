using System;
using System.Collections.Generic;

namespace DotNetty_ControllerBus
{
    /// <summary>
    /// 返回对象
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultType ResultType { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 实例化一个返回对象
        /// </summary>
        public ResultModel() { }
        /// <summary>
        /// 实例化一个返回对象
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        public ResultModel(ResultType resultType, string message)
        {
            ResultType = resultType;
            Message = message;
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultModel Success(string message)
        {
            return new ResultModel(ResultType.Success, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultModel Fail(string message)
        {
            return new ResultModel(ResultType.Fail, message);
        }
    }
    /// <summary>
    /// 返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel<T> : ResultModel
    {
        /// <summary>
        /// 携带数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 实例化一个返回对象
        /// </summary>
        public ResultModel() { }
        /// <summary>
        /// 实例化一个返回对象
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        public ResultModel(ResultType resultType, string message) : base(resultType, message) { }
        /// <summary>
        /// 实例化一个返回对象
        /// </summary>
        /// <param name="resultType">返回类型</param>
        /// <param name="message">返回消息</param>
        /// <param name="data">返回消息</param>
        public ResultModel(ResultType resultType, string message, T data) : base(resultType, message)
        {
            Data = data;
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static ResultModel<T> Success(string message)
        {
            return new ResultModel<T>(ResultType.Success, message);
        }
        /// <summary>
        /// 获得一个成功返回对象
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResultModel<T> Success(string message, T data)
        {
            return new ResultModel<T>(ResultType.Success, message, data);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public new static ResultModel<T> Fail(string message)
        {
            return new ResultModel<T>(ResultType.Fail, message);
        }
        /// <summary>
        /// 获得一个失败返回对象
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResultModel<T> Fail(string message, T data)
        {
            return new ResultModel<T>(ResultType.Fail, message, data);
        }
    }

    public class PageResultModel<Tentity> : ResultModel<List<Tentity>>
    {
        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 对象总数
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// 分页数量
        /// </summary>
        public int PageCount { get => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize); }
    }

}
