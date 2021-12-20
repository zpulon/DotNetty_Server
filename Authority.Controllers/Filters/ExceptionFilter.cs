
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty_Common;
using DotNetty_ControllerBus;
using DotNetty_ControllerBus.Filters;
using System;
using System.Text;

namespace Authority.Controllers.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public IFullHttpResponse HandlerException(IByteBufferHolder byteBufferHolder, IFullHttpResponse response, Exception exception)
        {
            ConsoleHelper.ServerWriteError(exception);
            ResultModel resultModel = ResultModel.Fail("服务器错误，请联系后端工程师");
            byte[] bodyData = Encoding.UTF8.GetBytes(resultModel.ToJson());
            IByteBuffer byteBuffer = Unpooled.WrappedBuffer(bodyData);
            var result = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.InternalServerError, byteBuffer);
            result.Headers.Set(HttpHeaderNames.Date, DateTime.Now);
            result.Headers.Set(HttpHeaderNames.Server, "MateralDotNettyServer");
            result.Headers.Set(HttpHeaderNames.AcceptLanguage, "zh-CN,zh;q=0.9");
            result.Headers.Set(HttpHeaderNames.ContentType, "application/json");
            return result;
        }
    }
}
