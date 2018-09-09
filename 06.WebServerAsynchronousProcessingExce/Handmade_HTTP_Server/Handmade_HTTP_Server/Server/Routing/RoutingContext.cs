namespace Handmade_HTTP_Server.Server.Routing
{
    using Contracts;
    using System.Collections.Generic;
    using Handlers;
    
    public class RoutingContext : IRoutingContext
    {   
        public RoutingContext(RequestHandler handler, IEnumerable<string> parameters)
        {        
            this.Handler = handler;
            this.Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler Handler { get; private set; }
    }   
}       
               