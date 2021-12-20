using Authority.Domain.Repositories;
using Authority.MemoryRepository;
using Authority.Service;
using Authority.ServiceImpl;
using Microsoft.Extensions.DependencyInjection;

namespace Authority.DependencyInjection
{
    public static class AuthorityDIExtension
    {
        public static void AddAuthorityServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepositoryImpl>();
            services.AddTransient<IUserService, UserServiceImpl>();
            services.AddTransient<IJsonHelper, JsonHelper>();
            services.AddTransient<IAsyncJsonHelper, JsonHelper>();
            services.AddTransient<IChatSessionService, ChatSessionService>();
            services.AddSingleton(new RedisHelper(RedisConfig.ServerConfig.Connection, RedisConfig.ServerConfig.PassWord, RedisConfig.ServerConfig.InstanceName, RedisConfig.ServerConfig.DefaultDB));
        }
    }
}
