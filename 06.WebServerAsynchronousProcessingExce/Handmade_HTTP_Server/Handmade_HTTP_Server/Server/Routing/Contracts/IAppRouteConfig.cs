namespace Handmade_HTTP_Server.Server.Routing.Contracts
{
    using Handmade_HTTP_Server.Server.Enums;
    using Handmade_HTTP_Server.Server.Handlers;
    using System.Collections.Generic;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        void AddRoute(string route, RequestHandler requestHandler); 
    }

}
