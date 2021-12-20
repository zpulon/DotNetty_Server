using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using DotNetty_CommandBus;
using DotNetty_Commands;
using DotNetty_Common;
using System.Threading.Tasks;

namespace DotNetty_CommandHandlers
{
    public class Test01CommandHandler : BaseCommandHandler<Test01Command>
    {
        public override async Task HandlerAsync(ICommand command, IChannel channel)
        {
            await Task.Run(() =>
            {
                if (!(command is Test01Command trueCommand)) return;
                ConsoleHelper.ServerWriteLine($"接到命令{trueCommand.Command}");
                ConsoleHelper.ServerWriteLine($"{trueCommand.ToJson()}");
                channel.WriteAndFlushAsync(new TextWebSocketFrame($"命令{trueCommand.Command}已处理"));
            });
        }
    }
}
