namespace DotNetty_CommandBus
{
    public interface ICommandBus
    {
        /// <summary>
        /// 获得命令处理器
        /// </summary>
        /// <param name="commandHandlerName"></param>
        /// <returns></returns>
        ICommandHandler GetCommandHandler(string commandHandlerName);
    }
}
