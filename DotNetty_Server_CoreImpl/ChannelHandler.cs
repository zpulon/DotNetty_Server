using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace DotNetty_Server_CoreImpl
{
    public class ChannelHandler : SimpleChannelInboundHandler<IByteBufferHolder>
    {
        private readonly WebSocketHandler _webSocketHandler;
        private readonly FileHandler _fileHandler;
        private readonly WebAPIHandler _webAPIHandler;
        public event Action<Exception> OnException;
        public ChannelHandler(WebSocketHandler webSocketHandler, FileHandler fileHandler, WebAPIHandler webAPIHandler)
        {
            _webSocketHandler = webSocketHandler;
            _webAPIHandler = webAPIHandler;
            _fileHandler = fileHandler;
        }
        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBufferHolder byteBuferrHolder)
        {
            byteBuferrHolder.Retain(byte.MaxValue);
            Task.Run(async () =>
            {
                try
                {
                    await GetHandler().HandlerAsync(ctx, byteBuferrHolder);
                }
                catch (Exception exception)
                {
                    OnException?.Invoke(exception);
                }
            });
        }
        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <returns></returns>
        private HandlerContext GetHandler()
        {
            var handlers = new HandlerContext[]
            {
                _webSocketHandler,
                _webAPIHandler,
                _fileHandler
            };
            for (var i = 0; i < handlers.Length; i++)
            {
                handlers[i].ShowException = OnException;
                if(i - 1 >= 0)
                {
                    handlers[i - 1].SetNext(handlers[i]);
                }
            }
            return handlers[0];
        }
    }
}
