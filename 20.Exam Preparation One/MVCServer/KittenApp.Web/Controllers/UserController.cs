namespace KittenApp.Web.Controllers
{
    using KittenApp.Data;
    using KittenApp.Models;
    using KittenApp.Web.BindingModels;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using SimpleMvc.Framework.Security;

    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            this.Model["error"] = "";
            return View();
        }
        
        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {

            if (!this.IsValidModel(registerUserBindingModel))
            {
                this.Model["error"] = "<h1 class=\"message\">Something went wrong !</h1>";
                return View();
            }

            if (registerUserBindingModel.Password == ""
                || registerUserBindingModel.ConfirmPassword == ""
                || registerUserBindingModel.Username == "")
            {
                this.Model["error"] = "<h1 class=\"message\">Empty input fields !</h1>";
                return View();
            }

            if (registerUserBindingModel.Password != registerUserBindingModel.ConfirmPassword)
            {

                this.Model["error"] = "<h1 class=\"message\">Passwords must match !</h1>";
                return View();
            }

            var user = new User()
            {
                Email = registerUserBindingModel.Email, 
                Username = registerUserBindingModel.Username,
                Password = PasswordUtilities.GenerateHash256(registerUserBindingModel.Password)
            };

            //add user to db
            using (this.Context)
            {
                if (Context.Users.Any(u => u.Email == registerUserBindingModel.Email))
                {
                    this.Model["error"] = "<h1 class=\"message\">Email is taken !</h1>";
                    return View();
                }

                Context.Users.Add(user);
                Context.SaveChanges();
                this.SignIn(user.Username);
            }

            this.SignIn(user.Username);

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.Model["error"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel loginUserBindingModel)
        {

            if (loginUserBindingModel.Username == "" || loginUserBindingModel.Password == "")
            {
                this.Model["error"] = "<h1 class=\"message\">Empty input fields !</h1>";
                return View();
            }
            
                var foundUser = this.Context
                    .Users
                    .FirstOrDefault(u => u.Username == loginUserBindingModel.Username
                        && u.Password == PasswordUtilities.GenerateHash256(loginUserBindingModel.Password));

                if (foundUser == null)
                {
                    this.Model["error"] = "<h1 class=\"message\">Invalid Username or password !</h1>";
                    return View();
                }

                //we add it to the session storage
                this.SignIn(foundUser.Username);
                this.User= new Authentication( foundUser.Username);

                Context.SaveChanges();

                return RedirectToAction("/home/index");
        
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            this.SignOut();
            return RedirectToAction("/home/index");
        }
    }
}
