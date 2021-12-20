using Authority.Controllers.Models;
using Authority.Domain;
using Authority.Service;
using Authority.Service.Models.User;
using DotNetty_Common;
using DotNetty_ControllerBus;
using DotNetty_ControllerBus.Attributes;

namespace Authority.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Register(RegisterUserRequestModel requestModel)
        {
            try
            {
                var model = new RegisterModel
                {
                    Account = requestModel.Account,
                    Name = requestModel.Name,
                    Password = requestModel.Password
                };
                _userService.Register(model);
                return ResultModel.Success("注册成功");
            }
            catch (DotNettyServerException exception)
            {
                return ResultModel.Fail(exception.Message);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<string> Login(LoginUserRequestModel requestModel)
        {
            try
            {
                User user = _userService.Login(requestModel.Account, requestModel.Password);
                return ResultModel<string>.Fail("登录成功", user.ID.ToString());
            }
            catch (DotNettyServerException exception)
            {
                return ResultModel<string>.Fail(exception.Message);
            }
        }
    }
}
