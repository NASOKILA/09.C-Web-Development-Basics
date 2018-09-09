
namespace Handmade_HTTP_Server.Server.Contracts
{
    using Routing.Contracts;

    public interface IApplication
    {
        void Configure(IAppRouteConfig appRouteConfig);
    } 
}
