using SimpleMvs.Framework.Interfaces;
using System.Text;

namespace SimpleMvc.App.Views.Home
{

    public class Index : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h1>Hello MVC !</h1>");
            sb.AppendLine("<h3>Notes App</h3>");
            sb.AppendLine("<a href=\"/users/all\">All Users</a>");
            sb.AppendLine("<a href=\"/users/register\">Register Users</a>");
            
            return sb.ToString();
        }
    }
}
