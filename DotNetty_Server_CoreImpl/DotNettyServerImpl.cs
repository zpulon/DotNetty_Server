
using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty_Common;
using DotNetty_Server_Core;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotNetty_Server_CoreImpl
{
    public class DotNettyServerImpl : IDotNettyServer
    {
        public event Action<string> OnMessage;
        public event Action<string, string> OnSubMessage;
        public event Action<Exception> OnException;
        public event Func<string> OnGetCommand;
        private readonly static ConcurrentDictionary<string, IChannel> servers = new();
        public async Task RunServerAsync()
        {
            OnSubMessage?.Invoke("服务启动中......", "重要");
            //第一步：创建ServerBootstrap实例
            var bootstrap = new ServerBootstrap();
            //第二步：绑定事件组
            IEventLoopGroup mainGroup = new MultithreadEventLoopGroup(1);
            IEventLoopGroup workGroup = new MultithreadEventLoopGroup();
            bootstrap.Group(mainGroup, workGroup);
            //第三步：绑定服务端的通道
            bootstrap.Channel<TcpServerSocketChannel>();
            //第四步：配置处理器
            bootstrap.Option(ChannelOption.SoBacklog, 8192);
            bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast(new HttpServerCodec());
                pipeline.AddLast(new HttpObjectAggregator(65536));
                var channelHandler = ApplicationService.GetService<ChannelHandler>();
                if(OnException != null)
                {
                    channelHandler.OnException += OnException;
                }
                pipeline.AddLast(channelHandler);
            }));
            //第五步：配置主机和端口号
            foreach (var  server in ApplicationConfig.ServerConfig)
            {
                IChannel bootstrapChannel = await bootstrap.BindAsync(IPAddress.Parse(server.Host), server.Port);
                servers.TryAdd($"{server.Host}:{server.Port}", bootstrapChannel);
            }

            OnSubMessage?.Invoke("服务启动成功", "重要");
            foreach (var server in ApplicationConfig.ServerConfig)
            {
                OnMessage?.Invoke($"已监听http://{server.Host}:{server.Port}");
            }
           
            
            //第六步：停止服务
            WaitServerStop();
            OnSubMessage?.Invoke("正在停止服务......", "重要");
            foreach (var server in servers)
            {
                await server.Value.CloseAsync();
                OnSubMessage?.Invoke($"{server.Key}服务已停止", "重要");
            }
            
           
        }
        #region 私有方法
        /// <summary>
        /// 等待服务停止
        /// </summary>
        private void WaitServerStop()
        {
            OnMessage?.Invoke("输入Stop停止服务");
            string inputKey = string.Empty;
            while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
            {
                inputKey = OnGetCommand?.Invoke();
                if (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                {
                    OnException?.Invoke(new DotNettyServerException("未识别命令请重新输入"));
                }
            }
        }
     
        #endregion
    }
}
