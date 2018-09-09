namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using System;
    using Infrastructure;
    using Models;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using HTTPServer.Server.Utilities;
    using HTTPServer.ByTheCakeApplication.Data;
    using System.Linq;

    public class AccountController : Controller
    {
        private const string INVALID_NAME_OR_PASSWORD_MESSAGE = "Invalid name or password";
        private const string EMPTY_FIELDS_ERROR_MESSAGE = "You have empty fields";
        private const string PASSWORD_MATCH_ERROR_MESSAGE = "Passwords should match";
        private const string NAME_AND_USERNAME_VALIDATION_ERROR_MESSAGE = "Name or Username is less than 3 symbols";


        public IHttpResponse Register()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\register");
        }

        public IHttpResponse Register(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formUsernameKey = "username";
            const string formPasswordKey = "password";
            const string formConfirmPasswordKey = "confirmpassword";

           if (!req.FormData.ContainsKey(formNameKey)
                || !req.FormData.ContainsKey(formUsernameKey)
                || !req.FormData.ContainsKey(formPasswordKey)
                || !req.FormData.ContainsKey(formConfirmPasswordKey))
            {
                RejectLoginAttempt(EMPTY_FIELDS_ERROR_MESSAGE);
                return this.FileViewResponse(@"account\register");
            }

            var name = req.FormData[formNameKey];
            var username = req.FormData[formUsernameKey];
            var password = req.FormData[formPasswordKey];
            var confirmpassword = req.FormData[formConfirmPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password)
                || string.IsNullOrWhiteSpace(confirmpassword))
            {
                RejectLoginAttempt(EMPTY_FIELDS_ERROR_MESSAGE);
                return this.FileViewResponse(@"account\register");
            }

            if (name.Length < 3 || username.Length < 3){

                RejectLoginAttempt(NAME_AND_USERNAME_VALIDATION_ERROR_MESSAGE);
                return this.FileViewResponse(@"account\register");
            }


            if (password != confirmpassword)
            {
                RejectLoginAttempt(PASSWORD_MATCH_ERROR_MESSAGE);
                return this.FileViewResponse(@"account\register");
            }

            User user = new User()
            {
                Name = name,
                Username = username,
                PasswordHash = PasswordUtilities.GenerateHash256(password),
                DateOfRegistration = DateTime.UtcNow
            };

            using (var context = new ByTheCakeContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return LoginUser(req, user);
        }
        
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey)
                || !req.FormData.ContainsKey(formPasswordKey))
            {
                RejectLoginAttempt(EMPTY_FIELDS_ERROR_MESSAGE);

                return this.FileViewResponse(@"account\login");
            }

            var name = req.FormData[formNameKey];
            var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(password))
            {
                RejectLoginAttempt(EMPTY_FIELDS_ERROR_MESSAGE);

                return this.FileViewResponse(@"account\login");
            }

            using (var context = new ByTheCakeContext())
            {
                User user = context.Users
                    .FirstOrDefault(u => u.Name == name && u.PasswordHash == PasswordUtilities.GenerateHash256(password));

                if (user == null)
                {
                    RejectLoginAttempt(INVALID_NAME_OR_PASSWORD_MESSAGE);

                    return this.FileViewResponse(@"account\login");
                }
                return LoginUser(req, user);
            }
            
        }
        
        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }


        private void RejectLoginAttempt(string message)
        {
            this.ViewData["error"] = message;
            this.ViewData["showError"] = "block";
            this.ViewData["authDisplay"] = "none";
        }

        private static IHttpResponse LoginUser(IHttpRequest req, User user)
        {
            req.Session.Add(SessionStore.CurrentUserKey, user.Id);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }
    }
}
