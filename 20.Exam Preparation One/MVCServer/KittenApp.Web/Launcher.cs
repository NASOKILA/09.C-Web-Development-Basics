namespace KittenApp.Web
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using System;
    using WebServer;

    public class Launcher
    {
        static void Main(string[] args)
        {
            int port = 3000;
            var context = new KittenAppDbContext();

            context.Database.Migrate();

            var server = new WebServer(port,
                new ControllerRouter(),
                new ResourceRouter());

            Console.WriteLine("Database Ready !");

            MvcEngine.Run(server, context);
        }
    }
}
