using DotNetty.Codecs.Http;
using System;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public abstract class BaseAuthorityFilterAttribute : Attribute, IAuthorityFilter
    {
        public abstract void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, IFullHttpResponse response);
    }
}
