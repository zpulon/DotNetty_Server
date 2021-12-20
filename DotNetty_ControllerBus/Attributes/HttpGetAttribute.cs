namespace DotNetty_ControllerBus.Attributes
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() : base("Get") { }
    }
}
