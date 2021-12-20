using System;

namespace DotNetty_Common
{
    public class DotNettyServerException : Exception
    {
        public DotNettyServerException()
        {
        }

        public DotNettyServerException(string message) : base(message)
        {
        }

        public DotNettyServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
