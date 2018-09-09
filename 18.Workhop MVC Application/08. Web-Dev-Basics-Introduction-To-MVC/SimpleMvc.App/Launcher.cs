namespace SimpleMvc.App
{
    using WebServer;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Framework.Routers;
    using Framework;

    public class Launcher
    {
        static void Main(string[] args)
        {
            var context = new NotesDbContext();

            context.Database.Migrate();
            
            var server = new WebServer(8000, 
                new ControllerRouter(), 
                new ResourceRouter());

            MvcEngine.Run(server);
        }
    }
}
