namespace HTTPServer.Application.Views.Account
{
    using Server.Contracts;
    
    public class LoginView : IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/login.html");
            return result;
        }
    }
}
