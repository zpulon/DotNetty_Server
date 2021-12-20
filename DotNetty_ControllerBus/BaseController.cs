
using DotNetty_ControllerBus.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using DotNetty_ControllerBus.Filters;
using DotNetty_Common;

namespace DotNetty_ControllerBus
{
    public abstract class BaseController
    {
        public ActionInfo GetAction(string key)
        {
            Type controllerType = GetType();
            MethodInfo[] methodInfos = controllerType.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                var routeAttribute = methodInfo.GetCustomAttribute<RouteAttribute>();
                if(routeAttribute != null && string.Equals(key, routeAttribute.Key, StringComparison.CurrentCultureIgnoreCase) ||
                    routeAttribute == null && string.Equals(key, methodInfo.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return new ActionInfo(this, methodInfo);
                }
            }
            throw new DotNettyServerException("未找到对应的Action");
        }

        /// <summary>
        /// 处理控制器过滤器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task HandlerControllerAfterFilterAsync(IFullHttpRequest request, IFullHttpResponse response, params IFilter[] globalFilters)
        {
            List<Attribute> attributes = GetType().GetCustomAttributes().ToList();
            List<IControllerAfterFilter> filters = attributes.OfType<IControllerAfterFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IControllerAfterFilter>());
            }
            foreach (IControllerAfterFilter filter in filters)
            {
                filter.HandlerFilter(this, request, response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return;
            }
            List<IControllerAfterAsyncFilter> asyncFilters = attributes.OfType<IControllerAfterAsyncFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IControllerAfterAsyncFilter>());
            }
            foreach (IControllerAfterAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(this, request, response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return;
            }
        }

        /// <summary>
        /// 处理控制器过滤器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task<IFullHttpResponse> HandlerControllerBeforeFilterAsync(IFullHttpRequest request, params IFilter[] globalFilters)
        {
            IFullHttpResponse response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.OK);
            List<Attribute> attributes = GetType().GetCustomAttributes().ToList();
            List<IControllerBeforeFilter> filters = attributes.OfType<IControllerBeforeFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IControllerBeforeFilter>());
            }
            foreach (IControllerBeforeFilter filter in filters)
            {
                filter.HandlerFilter(this, request, response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            List<IControllerBeforeAsyncFilter> asyncFilters = attributes.OfType<IControllerBeforeAsyncFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IControllerBeforeAsyncFilter>());
            }
            foreach (IControllerBeforeAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(this, request, response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            return response;
        }
    }
}
