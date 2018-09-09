namespace Handmade_HTTP_Server.Server.HTTP
{
    using Contracts;

    public class HttpContext : IHttpContext
    {
        private readonly HttpRequest request;

        public HttpContext(HttpRequest request)
        {
            
            this.request = request;
        }

        public IHttpRequest Request => this.request;
    }
}
