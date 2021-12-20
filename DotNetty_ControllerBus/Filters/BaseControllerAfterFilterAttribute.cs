using System;
using DotNetty.Codecs.Http;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BaseControllerAfterFilterAttribute : Attribute, IControllerAfterFilter
    {
        public abstract void HandlerFilter(BaseController baseController, IFullHttpRequest request, IFullHttpResponse response);
    }
}
