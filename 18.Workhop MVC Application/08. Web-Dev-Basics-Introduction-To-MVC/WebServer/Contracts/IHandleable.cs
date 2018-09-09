
namespace WebServer.Contracts
{
    using Http.Contracts;
    
    //transform a HTTP request to HTTP response
    public interface IHandleable
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
