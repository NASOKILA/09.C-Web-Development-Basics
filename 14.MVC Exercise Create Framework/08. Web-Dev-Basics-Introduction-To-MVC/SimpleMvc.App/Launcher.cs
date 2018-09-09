namespace SimpleMvc.App
{
    using SimpleMvs.Framework.Routers;
    using WebServer;
    using SimpleMvs.Framework;
    using SimpleMvc.Data;
    using Microsoft.EntityFrameworkCore;

    public class Launcher
    {
        static void Main(string[] args)
        {
            var context = new NotesDbContext();
            context.Database.Migrate();

            var server = new WebServer(8000, new ControllerRouter());
            MvcEngine.Run(server);
        }
    }
}
