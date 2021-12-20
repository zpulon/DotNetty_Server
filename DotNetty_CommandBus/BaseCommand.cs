namespace DotNetty_CommandBus
{
    public class BaseCommand : ICommand
    {
        public string Command { get; set; }

        public string CommandHandler => $"{Command}Handler";
    }
}
