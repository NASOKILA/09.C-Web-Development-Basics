namespace HTTPServer.ByTheCakeApplication
{
    using Controllers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            ConfigureDatabase();

            ConfigureRoutes(appRouteConfig);
        }

        private static void ConfigureDatabase()
        {

            //INICIALIZIRAME SI BAZATA TUK ZA DA Q POLZVAME 
            var context = new ByTheCakeContext();

            //Taka Pravim migraciqta Obache trqbva da q napravim smo vednuj a ne vseki put
            context.Database.Migrate();
        }

        private static void ConfigureRoutes(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get(
                    "/", 
                    req => new HomeController().Index(req));

            appRouteConfig
                .Get(
                    "/about", 
                    req => new HomeController().About(req));

            appRouteConfig
                .Get(
                    "/profile", 
                    req => new HomeController().Profile(req));

            appRouteConfig
                .Get(
                    "/orders",
                    req => new HomeController().Orders(req));

            appRouteConfig
                .Get(
                    "/add",
                    req => new CakesController().Add());

            appRouteConfig
                .Post(
                    "/add",
                    req => new CakesController().Add(req));

            appRouteConfig
                .Get(
                    "/search",
                    req => new CakesController().Search(req));
            
            appRouteConfig
                .Get(
                    "/register",
                    req => new AccountController().Register());

            appRouteConfig
                .Post(
                    "/register",
                    req => new AccountController().Register(req));
            
            appRouteConfig
                .Get(
                    "/login",
                    req => new AccountController().Login());

            appRouteConfig
                .Post(
                    "/login",
                    req => new AccountController().Login(req));
            
            appRouteConfig
                .Post(
                    "/logout",
                    req => new AccountController().Logout(req));

            appRouteConfig
                .Get(
                    "/shopping/add/{(?<id>[0-9]+)}",
                    req => new ShoppingController().AddToCart(req));


            appRouteConfig
                .Get(
                    "/orderDetails/{(?<id>[0-9]+)}",
                    req => new CakesController().OrderDetails(req));


            appRouteConfig
                .Get(
                    "/cakeDetails/{(?<id>[0-9]+)}",
                    req => new CakesController().CakeDetails(req));
            
            appRouteConfig
                .Get(
                    "/cart",
                    req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post(
                    "/shopping/finish-order",
                    req => new ShoppingController().FinishOrder(req));
        }
    }
}
