
using Authority.Controllers.Filters;
using Authority.DependencyInjection;
using DotNetty_ControllerBus;
using DotNetty_Server_Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotNetty_Server_CoreImpl
{
    public static class ServerCoreDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddServerCore(this IServiceCollection services)
        {
            services.AddTransient<IDotNettyServer, DotNettyServerImpl>();
            services.AddTransient<ChannelHandler>();
            services.AddTransient<WebSocketHandler>();
            services.AddTransient<FileHandler>();
            services.AddTransient<WebAPIHandler>();
            services.AddTransient<ConnectionManager>();
            services.AddAuthorityServices();
            services.AddControllerBus(controllerHelper =>
            {
                controllerHelper.AddFilter<ExceptionFilter>();
            }, Assembly.Load("Authority.Controllers"));
        }
    }
}
