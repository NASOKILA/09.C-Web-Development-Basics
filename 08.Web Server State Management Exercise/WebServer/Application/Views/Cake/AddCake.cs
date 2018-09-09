namespace HTTPServer.Application.Views.Cake
{
    using HTTPServer.Server.Contracts;
    
    public class AddCake : IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/add.html");
            return result;
        }
    }
}
