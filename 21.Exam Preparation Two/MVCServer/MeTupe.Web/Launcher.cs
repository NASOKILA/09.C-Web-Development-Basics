namespace MeTupe.Web
{
    using MeTube.Data;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using System;
    using WebServer;

    public class Launcher
    {
        static void Main(string[] args)
        {
            int port = 8000;

            var context = new MeTubeDbContext();

            context.Database.Migrate();

            Console.WriteLine("Database Ready!");

            var server = new WebServer(port, 
                new ControllerRouter(),
                new ResourceRouter());

            MvcEngine.Run(server, context);
        }
    }
}
