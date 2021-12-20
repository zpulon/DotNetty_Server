using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace DotNetty_ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public abstract class BaseAuthorityAfterAsyncFilterAttribute : Attribute, IAuthorityAsyncFilter
    {
        public abstract Task HandlerFilterAsync(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, IFullHttpResponse response);
    }
}
