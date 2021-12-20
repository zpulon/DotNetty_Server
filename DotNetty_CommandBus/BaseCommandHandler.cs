
using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace DotNetty_CommandBus
{
    public abstract class BaseCommandHandler<T> : ICommandHandler
    {
        public Type CommandType => typeof(T);
        public abstract Task HandlerAsync(ICommand command, IChannel channel);
    }
}
