using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BaseControllerBeforeAsyncFilterAttribute : Attribute, IControllerBeforeAsyncFilter
    {
        public abstract Task HandlerFilterAsync(BaseController baseController, IFullHttpRequest request, IFullHttpResponse response);
    }
}
