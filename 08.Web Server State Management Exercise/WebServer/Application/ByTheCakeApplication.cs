namespace HTTPServer.Application
{
    using Server.Contracts;
    using Server.Routing.Contracts;
    using Controllers;

    public class ByTheCakeApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {

            appRouteConfig
               .Get("/", req => new HomeController().Index(req));

            appRouteConfig
               .Get("/style.css", req => new HomeController().GetCss());

            appRouteConfig
               .Get("/about-us", req => new HomeController().AboutView(req));

            appRouteConfig
               .Get("/add", req => new HomeController().AddCake(req));

            appRouteConfig
               .Post("/add", req => new HomeController()
               .RegisterCake(req.FormData["name"], req.FormData["price"]));

            appRouteConfig
               .Get("/search", req => new HomeController().Search(req));

            appRouteConfig
               .Post("/search", req => new HomeController().SearchCake(req.FormData["cake"]));

            appRouteConfig
               .Get("/login", req => new AccountController().Login());

            appRouteConfig
               .Post("/login", req => new AccountController()
               .LoginPost(req, req.FormData["username"], req.FormData["password"]));

            appRouteConfig
               .Get("/logout", req => new AccountController().Logout(req));



            appRouteConfig
               .Post("/order", req => new AccountController()
               .OrderPost(req, req.FormData["orderedCake"]));
            
            appRouteConfig
               .Get("/success", req => new AccountController().Success(req));

            appRouteConfig
               .Get("/cart", req => new AccountController().Cart(req));

        }
    }
}
