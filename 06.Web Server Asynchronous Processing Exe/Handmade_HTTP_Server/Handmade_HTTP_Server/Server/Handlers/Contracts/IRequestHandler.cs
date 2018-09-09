namespace Handmade_HTTP_Server.Server.Handlers.Contracts
{
    using HTTP.Contracts;
    
    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext context);
    }
}
