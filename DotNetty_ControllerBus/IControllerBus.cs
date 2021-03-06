using DotNetty_ControllerBus.Filters;

namespace DotNetty_ControllerBus
{
    public interface IControllerBus
    {
        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        BaseController GetController(string key);
        /// <summary>
        /// 获取全局过滤器
        /// </summary>
        /// <returns></returns>
        IFilter[] GetGlobalFilters();
    }
}
