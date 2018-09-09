namespace Handmade_HTTP_Server.Server.Routing
{
    using Contracts;
    using System.Collections.Generic;
    using Enums;
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var avaliableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in avaliableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeRouteConfig(appRouteConfig); 
        }

        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;
        
        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes => routes;

        private void InitializeRouteConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var registerRoute in appRouteConfig.Routes)
            {
                var requestMethod = registerRoute.Key;
                var routesWithHandler = registerRoute.Value;

                foreach (var routeWithHandler in routesWithHandler)
                {
                    var route = routeWithHandler.Key;
                    var handler = routeWithHandler.Value;
                    var parameters = new List<string>();

                    var parsedRouteRegex = this.ParseRoute(route, parameters);
                    
                    var routingContext = new RoutingContext(handler, parameters);

                    this.routes[requestMethod].Add(parsedRouteRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var result = new StringBuilder();
            result.Append("^");

            if (route == "/")
            {
                result.Append("/$");
                return result.ToString();
            }

            var tokens = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            tokens[0] = '/' + tokens[0];

            this.ParseTokens(tokens, parameters, result);

            return result.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder result)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = tokens.Length - 1 == i ? "$" : "/";
                var currentToken = tokens[i];

                if (!currentToken.StartsWith('{') && !currentToken.EndsWith('}'))
                {
                    result.Append($"{currentToken}{end}");
                    continue;
                }

                var parameterRegex = new Regex("<\\w+>");

                var parameterMatch = parameterRegex.Match(currentToken);

                if(!parameterMatch.Success) {
                    throw new InvalidOperationException($"Route parameter in {currentToken} is not valid.");
                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);

                parameters.Add(parameter);

                var currentTokenWithNoBrackets = currentToken.Substring(1, currentToken.Length-2);

                result.Append($"{currentTokenWithNoBrackets}{end}");
            }
        }
    }

}
