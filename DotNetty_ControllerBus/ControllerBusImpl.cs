
using System;
using DotNetty_ControllerBus.Filters;
using DotNetty_Common;

namespace DotNetty_ControllerBus
{
    public class ControllerBusImpl : IControllerBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ControllerHelper _controllerHelper;
        public ControllerBusImpl(IServiceProvider serviceProvider, ControllerHelper controllerHelper)
        {
            _serviceProvider = serviceProvider;
            _controllerHelper = controllerHelper;
        }
        public BaseController GetController(string key)
        {
            Type type = _controllerHelper.GetController(key);
            if (type == null) throw new DotNettyServerException("未找到控制器");
            object controller = _serviceProvider.GetService(type);
            if(controller == null) throw new DotNettyServerException($"未找到控制器{type.FullName}");
            if(controller is not BaseController baseController) throw new DotNettyServerException($"控制器必须继承类{nameof(BaseController)}");
            return baseController;
        }

        public IFilter[] GetGlobalFilters()
        {
            return _controllerHelper.GetAllFilters();
        }
    }
}
