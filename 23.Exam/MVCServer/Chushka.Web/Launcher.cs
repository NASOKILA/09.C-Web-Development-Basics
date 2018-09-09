namespace Chushka.Web
{
    using Chushka.Data;
    using SimpleMvc.Framework.Routers;
    using System;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Framework;
    using WebServer;

    public class Launcher
    {
        static void Main(string[] args)
        {
            int port = 8000;

            var context = new ChushkaDbContext();

            context.Database.Migrate();

            Console.WriteLine("Database Ready!");

            var server = new WebServer(port,
                new ControllerRouter(),
                new ResourceRouter());

            MvcEngine.Run(server, context);
        }
    }
}
