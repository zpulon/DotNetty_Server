using Authority.Domain;
using Authority.Service.Models.User;

namespace Authority.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        void Register(RegisterModel model);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Login(string account, string password);
    }
}
