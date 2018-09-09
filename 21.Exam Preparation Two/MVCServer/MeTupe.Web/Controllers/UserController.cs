namespace MeTupe.Web.Controllers
{

    using BindingModels;
    using MeTupe.Web.Attributes;
    using MeTybe.Models;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Security;
    using System.Linq;

    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            this.Model["error"] = "";

            return this.View();
        }
        
        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {

           

            if (registerUserBindingModel.Password == ""
                || registerUserBindingModel.ConfirmPassword == ""
                || registerUserBindingModel.Username == "")
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

            var user = new User()
            {
                Email = registerUserBindingModel.Email,
                Username = registerUserBindingModel.Username,
                Password = PasswordUtilities.GenerateHash256(registerUserBindingModel.Password)
            };
            


            using (this.Context)
            {
                if (Context.Users.Any(u => u.Email == registerUserBindingModel.Email))
                {
                    this.Model["error"] = "Email is taken !";
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
            return this.View();
        }
        
        [HttpPost]
        public IActionResult Login(LoginUserBindingModel loginUserBindingModel)
        {

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
            return RedirectToAction("/home/index");
        }

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Profile()
        {
            
            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            var myTubes = this.Context.Tubes
                .Include(u => u.Uploader)
                .Where(t => t.Uploader.Username == this.User.Name);

            int count = 1;
            string result = "";
            foreach (Tube tube in myTubes)
            {

                result += $@"<tr>
                                <th scope=""row"">{count}</th>
                                <td> {tube.Title} </td>
                                <td> {tube.Author} </td>
                                <td> <a href=""/tube/details?id={tube.Id}"">Details</a></td>
                            </tr>";
                count++;

            }
            
            this.Model["username"] = currentUser.Username;
            this.Model["email"] = currentUser.Email;
            this.Model["table"] = result;

            return this.View();
        }
    }
}
