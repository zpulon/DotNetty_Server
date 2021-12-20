using System;
using DotNetty.Codecs.Http;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseActionAfterFilterAttribute : Attribute, IActionAfterFilter
    {
        public abstract void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, IFullHttpResponse response);
    }
}
