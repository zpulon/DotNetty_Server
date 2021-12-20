

using DotNetty_CommandBus;

namespace DotNetty_Commands
{
    public class Test02Command : BaseCommand
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
    }
}
