namespace HTTPServer
{
    using Application;
    using Server;
    using Server.Routing;

    public class Launcher
    {
        static void Main(string[] args)
        {
            Run(args);
        }

        static void Run(string[] args)
        {
            //TODO: Initialize application
            var mainApplication = new ByTheCakeApplication();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);
            //TODO: Configure App Route Configuration

            var server = new WebServer(8000, appRouteConfig);

            server.Run();
        }
    }
}
