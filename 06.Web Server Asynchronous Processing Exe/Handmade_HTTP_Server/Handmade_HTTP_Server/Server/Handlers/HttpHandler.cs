namespace Handmade_HTTP_Server.Server.Handlers
{
    using Contracts;
    using Server.HTTP.Contracts;
    using Routing.Contracts;
    using System.Text.RegularExpressions;
    using HTTP.Response;

    public class HttpHandler : IRequestHandler
    {

        private readonly IServerRouteConfig routeConfig;

        public HttpHandler(IServerRouteConfig routeConfig)
        {
            this.routeConfig = routeConfig;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            var requestMethod = context.Request.RequestMethod;
            var requestPath = context.Request.Path;
            var registeredRoutes = this.routeConfig.Routes[requestMethod];

            foreach (var registeredRoute in registeredRoutes)
            {

                var routePattern = registeredRoute.Key;
                var routingContext = registeredRoute.Value;

                var routeRegex = new Regex(routePattern);
                var match = routeRegex.Match(requestPath);

                if (!match.Success)
                {
                    continue;
                }

                var parameters = routingContext.Parameters;
                foreach (var parameter in parameters)
                {
                    var parameterValue = match.Groups[parameter].Value;

                    context.Request.AddUrlParameter(parameter, parameterValue);
                }
                
                return routingContext.Handler.Handle(context);
            }

            return new NotFoundResponse();

        }
    }
}
