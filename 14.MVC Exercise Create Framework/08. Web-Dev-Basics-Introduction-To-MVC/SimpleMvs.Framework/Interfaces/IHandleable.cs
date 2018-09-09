
namespace SimpleMvs.Framework.Interfaces
{
    using WebServer.Http.Contracts;
    
    public interface IHandleable
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
