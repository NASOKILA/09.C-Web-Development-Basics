namespace Handmade_HTTP_Server
{
    using Server.Contracts;
    using Server;
    using Server.Routing;
    using Application;

    public class Launcher : IRunnable
    {
        

        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new MainApplication();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            WebServer webServer = new WebServer(8230, appRouteConfig);

            webServer.Run();
        }
    }
}
