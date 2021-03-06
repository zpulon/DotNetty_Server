using System;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;

namespace DotNetty_Server_CoreImpl
{
    public class WebSocketHandler : HandlerContext
    {
        private const string WebSocketUrl = "/websocket/chat";
        private ConnectionManager _connectionManager { get; set; }
        public WebSocketHandler(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        private WebSocketServerHandshaker _handShaker;
        public override async Task HandlerAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder)
        {
            if (byteBufferHolder is IFullHttpRequest request && request.Uri.Equals(WebSocketUrl, StringComparison.OrdinalIgnoreCase))
            {
     
                await ProtocolUpdateAsync(ctx, request);
               
            }
            else
            {
                await HandlerAsync<WebSocketFrame>(ctx, byteBufferHolder, HandlerRequestAsync);
            }
        }
        #region 私有方法
        /// <summary>
        /// 获取WebSocket地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetWebSocketAddress(IFullHttpRequest request)
        {
            request.Headers.TryGet(HttpHeaderNames.Host, out ICharSequence value);
            string address = "ws://" + value.ToString() + WebSocketUrl;
            return address;
        }
        /// <summary>
        /// 协议升级
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="request"></param>
        private async Task ProtocolUpdateAsync(IChannelHandlerContext ctx, IFullHttpRequest request)
        {
            string address = GetWebSocketAddress(request);
            var webSocketServerHandshakerFactory = new WebSocketServerHandshakerFactory(address, null, true);
            _handShaker = webSocketServerHandshakerFactory.NewHandshaker(request);
            if (_handShaker == null)
            {
                await WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ctx.Channel);
            }
            else
            {
                
                await _handShaker.HandshakeAsync(ctx.Channel, request);
               // await _connectionManager.AddChannelHandlerAsync(ctx, "", "", "");
            }
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        private async Task HandlerRequestAsync(IChannelHandlerContext ctx, WebSocketFrame frame)
        {
            switch (frame)
            {
                //关闭
                case CloseWebSocketFrame closeWebSocketFrame:
                    if (_handShaker == null) return;
                    await _handShaker.CloseAsync(ctx.Channel, closeWebSocketFrame);
                    break;
                //心跳->Ping
                case PingWebSocketFrame _:
                    await ctx.WriteAndFlushAsync(new PongWebSocketFrame());
                    break;
                //心跳->Pong
                case PongWebSocketFrame _:
                    await ctx.WriteAndFlushAsync(new PingWebSocketFrame());
                    break;
                //文本
                case TextWebSocketFrame textWebSocketFrame:
                    await ctx.WriteAndFlushAsync(new TextWebSocketFrame($"服务器返回消息:{textWebSocketFrame.Content.ToString(Encoding.UTF8)}"));
                   // await _connectionManager.GE
                    break;
                //二进制
                case BinaryWebSocketFrame binaryWebSocketFrame:
                    await ctx.WriteAndFlushAsync(new BinaryWebSocketFrame(binaryWebSocketFrame.Content));
                    break;
            }
        }
        #endregion
    }
}
