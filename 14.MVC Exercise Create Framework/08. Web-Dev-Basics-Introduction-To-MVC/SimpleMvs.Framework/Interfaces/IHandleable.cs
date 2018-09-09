
namespace SimpleMvs.Framework.Interfaces
{
    using WebServer.Http.Contracts;
    
    //transform a HTTP request to HTTP response
    public interface IHandleable
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
