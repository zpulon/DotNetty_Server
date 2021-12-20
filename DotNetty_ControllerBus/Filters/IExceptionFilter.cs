using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using System;

namespace DotNetty_ControllerBus.Filters
{
    public interface IExceptionFilter : IFilter
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="byteBufferHolder"></param>
        /// <param name="response"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        IFullHttpResponse HandlerException(IByteBufferHolder byteBufferHolder, IFullHttpResponse response, Exception exception);
    }
}
