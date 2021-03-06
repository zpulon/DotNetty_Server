using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace DotNetty_ControllerBus.Filters
{
    public interface IExceptionAsyncFilter : IFilter
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="byteBufferHolder"></param>
        /// <param name="response"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Task<IFullHttpResponse> HandlerExceptionAsync(IByteBufferHolder byteBufferHolder, IFullHttpResponse response, Exception exception);
    }
}
