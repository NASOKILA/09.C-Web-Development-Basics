namespace HTTPServer.Application.Views.Home
{
    using HTTPServer.Server.Contracts;

    public class IndexView : IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/index.html");
            return result;
        }
    }
}
