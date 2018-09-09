namespace HTTPServer.Application.Views
{
    using HTTPServer.Server.Contracts;
    
    public class GetCssView : IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/style.css");
            return result;
        }
    }
}
