namespace Chushka.Web.Controllers
{
    using Chushka.Models;
    using Chushka.Web.BindingModels;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Security;
    using System.Collections.Generic;
    using System.Linq;

    public class UserController : BaseController
    {


        private bool userIsAdmin()
        {
            
            if (!this.User.IsAuthenticated)
            {
                return false;
            }

            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            this.Model["displayAnonymous"] = this.User.IsAuthenticated ? "none" : "block";

            if (currentUser.RoleId == 1)
            {
                return true;
            }

            return false;
        }


        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
                return RedirectToAction("/");

            //check if user is admin
            this.Model["isAdmin"] = userIsAdmin() ? "block" : "none";


            this.Model["error"] = "";
            return this.View();
        }
        
        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {

            if (this.User.IsAuthenticated)
                return RedirectToAction("/");

            //check if user is admin
            this.Model["isAdmin"] = userIsAdmin() ? "block" : "none";


            if (registerUserBindingModel.Password == ""
                || registerUserBindingModel.ConfirmPassword == ""
                || registerUserBindingModel.Username == ""
                || registerUserBindingModel.Email == ""
                || registerUserBindingModel.FullName == "")
            {
                this.Model["error"] = "Empty input fields !";
                return View();
            }

            if (registerUserBindingModel.Password != registerUserBindingModel.ConfirmPassword)
            {

                this.Model["error"] = "Passwords must match !";
                return View();
            }

            if (!this.IsValidModel(registerUserBindingModel))
            {
                this.Model["error"] = "Something went wrong !";
                return View();
            }


            var users = this.Context.Users.ToList();

            Role role = this.Context.Roles.FirstOrDefault(r => r.Name == "User");

            if (users.Count < 1) {
                role = this.Context.Roles.FirstOrDefault(r => r.Name == "Admin");
            }

            var user = new User()
            {
                RoleId = role.Id,
                Email = registerUserBindingModel.Email,
                Username = registerUserBindingModel.Username,
                Password = PasswordUtilities.GenerateHash256(registerUserBindingModel.Password),
                FullName = registerUserBindingModel.FullName,
                Orders = new List<Order>()
            };



            using (this.Context)
            {
                if (Context.Users.Any(u => u.Email == registerUserBindingModel.Email))
                {
                    this.Model["error"] = "Email is taken !";
                    return View();
                }

                if (Context.Users.Any(u => u.Username == registerUserBindingModel.Username))
                {
                    this.Model["error"] = "Username is taken !";
                    return View();
                }

                Context.Users.Add(user);

                Context.SaveChanges();

                this.SignIn(user.Username);

                return RedirectToAction("/");

            }
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
                return RedirectToAction("/");

            //check if user is admin
            this.Model["isAdmin"] = userIsAdmin() ? "block" : "none";
           


            this.Model["error"] = "";
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel loginUserBindingModel)
        {
            if (this.User.IsAuthenticated)
                return RedirectToAction("/");

            //check if user is admin
            this.Model["isAdmin"] = userIsAdmin() ? "block" : "none";


            if (!this.IsValidModel(loginUserBindingModel))
            {
                this.Model["error"] = "Something went wrong !";
                return View();
            }


            if (loginUserBindingModel.Username == "" || loginUserBindingModel.Password == "")
            {
                this.Model["error"] = "Empty input fields !";
                return View();
            }

            
            var foundUser = this.Context
                .Users
                .FirstOrDefault(u => u.Username == loginUserBindingModel.Username
                    && u.Password == PasswordUtilities.GenerateHash256(loginUserBindingModel.Password));

            if (foundUser == null)
            {
                this.Model["error"] = "Invalid Username or password !";
                return View();
            }

            this.SignIn(foundUser.Username);
            this.User = new Authentication(foundUser.Username);
            Context.SaveChanges();

            return RedirectToAction("/home/index");
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
           
            this.SignOut();
            return RedirectToAction("/");
        }
    }
}
