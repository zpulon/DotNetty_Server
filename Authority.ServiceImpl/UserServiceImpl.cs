using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Service;
using Authority.Service.Models.User;
using DotNetty_Common;

namespace Authority.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(RegisterModel model)
        {
            User userFromDB = _userRepository.FirstOrDefault(m => m.Account == model.Account);
            if(userFromDB != null) throw new DotNettyServerException("该帐号已被使用");
            userFromDB = new User
            {
                Account = model.Account,
                Name = model.Name,
                Password = model.Password
            };
            _userRepository.Add(userFromDB);
        }

        public User Login(string account, string password)
        {
            User userFromDB = _userRepository.FirstOrDefault(m => m.Account == account);
            if(userFromDB == null) throw new DotNettyServerException("帐号或密码错误");
            if(userFromDB.Password != password) throw new DotNettyServerException("帐号或密码错误");
            return userFromDB;
        }
    }
}
