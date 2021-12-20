using System;
using DotNetty.Codecs.Http;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BaseControllerBeforeFilterAttribute : Attribute, IControllerBeforeFilter
    {
        public abstract void HandlerFilter(BaseController baseController, IFullHttpRequest request, IFullHttpResponse response);
    }
}
