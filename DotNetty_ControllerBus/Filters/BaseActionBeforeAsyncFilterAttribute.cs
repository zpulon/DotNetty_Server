using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseActionBeforeAsyncFilterAttribute : Attribute, IActionBeforeAsyncFilter
    {
        public abstract Task HandlerFilterAsync(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, IFullHttpResponse response);
    }
}
