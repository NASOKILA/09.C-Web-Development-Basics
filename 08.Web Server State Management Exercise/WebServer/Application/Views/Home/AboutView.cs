namespace HTTPServer.Application.Views.Home
{
    using HTTPServer.Server.Contracts;
    
    public class AboutView : IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/about-us.html");
            return result;
        }
    }
}
