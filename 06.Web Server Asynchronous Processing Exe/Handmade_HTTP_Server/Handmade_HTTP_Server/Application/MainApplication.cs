namespace Handmade_HTTP_Server.Application
{
    using Server.Contracts;
    using Server.Routing.Contracts;
    using Server.Handlers;
    using Controllers;
    using Server.HTTP;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {

            appRouteConfig
                .AddRoute("/", new GetHandler(request => new HomeController().Index()));

            appRouteConfig
                .AddRoute("/register", new GetHandler(request => new UserController()
                .RegisterGet()));

            appRouteConfig
                .AddRoute("/register", 
                new PostHandler(
                    httpContext => new UserController()
                    .RegisterPost(httpContext.FormData["name"])));
            
            appRouteConfig
                .AddRoute("/user/{(?<name>[a-z]+)}", 
                new GetHandler(httpContext => new UserController()
                .Details(httpContext.UrlParameters["name"])));

        }
    }
}
