using DotNetty.Codecs.Http;

namespace DotNetty_ControllerBus.Filters
{
    public interface IControllerBeforeFilter : IFilter
    {
        /// <summary>
        /// 处理过滤器
        /// </summary>
        /// <param name="baseController"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HandlerFilter(BaseController baseController, IFullHttpRequest request, IFullHttpResponse response);
    }
}
