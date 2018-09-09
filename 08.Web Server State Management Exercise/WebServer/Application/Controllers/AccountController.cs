namespace HTTPServer.Application.Controllers
{
    using Views.Account;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Server.Enums;

    public class AccountController
    {

        private const string UserLoginToken = "{0}_UserLoggedinToken_$";

        public IHttpResponse Login()
        {
            var result = new ViewResponse(HttpStatusCode.Ok, new LoginView());
            return result;
        }
        
        public IHttpResponse LoginPost(IHttpRequest request, string username, string password)
        {
            //zapisvame usera v lokalnata sesiq
            SaveUserInSession(request, username);

            //vadim kookito
            var sessionCookie = request.Session.Get("UserLoginToken");
            
            return new RedirectResponse("/");
        }
        
        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();
            var sessionCookie = request.Session.Get("UserLoginToken");
            return new RedirectResponse("/login");
        }

        public IHttpResponse Success(IHttpRequest request)
        {

            if (!CheckIfLoggedIn(request))
                return new RedirectResponse("/login");

            System.IO.File.WriteAllText("./Application/Resourses/products.csv", "");


            var result = new ViewResponse(HttpStatusCode.Ok, new SuccessView());
            return result;
        }

        public IHttpResponse OrderPost(IHttpRequest request, string orderedCake)
        {

            if (!CheckIfLoggedIn(request))
                return new RedirectResponse("/login");

            string content = System.IO.File
                .ReadAllText("./Application/Resourses/products.csv").ToString();

            content += orderedCake + "\n";

            System.IO.File.WriteAllText("./Application/Resourses/products.csv", content);

            return new ViewResponse(HttpStatusCode.Ok, new ShowProducts());

        }

        public IHttpResponse Cart(IHttpRequest request)
        {

            if (!CheckIfLoggedIn(request))
                return new RedirectResponse("/login");
            
            var result = new ViewResponse(HttpStatusCode.Ok, new CartView());
            return result;
        }

        private static void SaveUserInSession(IHttpRequest request, string username)
        {
            request.Session.Add(nameof(UserLoginToken),
                string.Format(UserLoginToken, username));
        }
        
        private bool CheckIfLoggedIn(IHttpRequest request)
        {
            var sessionCookie = request.Session.Get("UserLoginToken");
            if (sessionCookie == null)
            {
                return false;
            }
            return true;
        }
    }
}
