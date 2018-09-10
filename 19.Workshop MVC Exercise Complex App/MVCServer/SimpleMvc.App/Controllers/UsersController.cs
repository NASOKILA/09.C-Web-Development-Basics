namespace NotesApp.Controllers
{
    using NotesApp.Models;
    using NotesApp.Models.BindingModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using System.Security.Cryptography;

    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisteringBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.View();
            }

            var sha256 = SHA256Managed.Create();
            var user = new User()
            {
                Username = model.Username,
                PasswordHash = string.Join(
                    "",
                    sha256
                        .ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password))
                        .Select(b => b.ToString("x2")))
            };

            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
            }

            this.SignIn(user.Username);
            return this.RedirectToAction("/home/index");
        }
    }
}
