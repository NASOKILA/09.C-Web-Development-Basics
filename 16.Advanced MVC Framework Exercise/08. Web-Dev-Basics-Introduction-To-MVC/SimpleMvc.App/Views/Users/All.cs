namespace SimpleMvc.App.Views.Users
{
    using SimpleMvc.App.ViewModels;
    using SimpleMvc.Data;
    using SimpleMvs.Framework.Interfaces;
    using System.Linq;
    using System.Text;

    public class All : IRenderable
    {
        public AllUsernamesViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<a href=\"/home/index\">Back Home</a>");
            sb.AppendLine("<h3> All Users </h3>");
            sb.AppendLine("<ul>");

            using (var context = new NotesDbContext())
            {
                var users = context.Users.ToList();
                
                foreach (var username in Model.Usernames)
                {
                    int userId = users
                        .FirstOrDefault(u => u.Username == username)
                        .Id;

                    sb.AppendLine($"<li><a href=\"/users/profile?id={userId}\">{username}</a></li>");
                }
            }

            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
