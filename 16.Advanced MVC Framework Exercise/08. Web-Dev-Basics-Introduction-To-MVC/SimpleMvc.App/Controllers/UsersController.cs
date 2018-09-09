namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using SimpleMvc.App.Utilities;
    using SimpleMvc.App.ViewModels;
    using SimpleMvc.Data;
    using SimpleMvc.Domain;
    using SimpleMvs.Framework.Attributes.Methods;
    using SimpleMvs.Framework.Controllers;
    using SimpleMvs.Framework.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {
            var user = new User()
            {
                Username = registerUserBindingModel.Username,
                Password = PasswordUtilities.GenerateHash256(registerUserBindingModel.Password)
            };
            
            using (var context = new NotesDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View();
        }
    }
}
