namespace NotesApp
{
    using Microsoft.EntityFrameworkCore;
    using NotesApp.Data;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main()
        {
            var server = new WebServer(
                42420, 
                new ControllerRouter(),
                new ResourceRouter());
            MvcEngine.Run(server, new NotesAppContext());
        }
    }
}
