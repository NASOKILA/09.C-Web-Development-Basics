namespace HTTPServer.Application.Views.Account
{
    using HTTPServer.Server.Contracts;
   
    public class SuccessView :IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/success.html");
            return result;
        }
    }
}
