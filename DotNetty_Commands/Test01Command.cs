
using DotNetty_CommandBus;

namespace DotNetty_Commands
{
    public class Test01Command : BaseCommand
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}
