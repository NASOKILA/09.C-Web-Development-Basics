namespace HTTPServer.Application.Controllers
{
    using Views.Home;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using HTTPServer.Application.Views;
    using HTTPServer.Application.Views.Cake;
    using HTTPServer.Server.Http;
    using System;

    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest request) {

            if (!CheckIfLoggedIn(request)) 
                return new RedirectResponse("/login");
            
            return new ViewResponse(HttpStatusCode.Ok, new IndexView());
        }
  
        public IHttpResponse GetCss()
        {
            return new ViewResponse(HttpStatusCode.Ok, new GetCssView());
        }

        public IHttpResponse AboutView(IHttpRequest request)
        {
            if (!CheckIfLoggedIn(request))
                return new RedirectResponse("/login");

            return new ViewResponse(HttpStatusCode.Ok, new AboutView());
        }

        public IHttpResponse AddCake(IHttpRequest request)
        {
            if (!CheckIfLoggedIn(request))
                return new RedirectResponse("/login");

            return new ViewResponse(HttpStatusCode.Ok, new AddCake());
        }

        public IHttpResponse RegisterCake(string name, string price)
        {

            var cake = new Cake(name, price);

            string content = System.IO.File.ReadAllText("./Application/Resourses/database.csv").ToString();

            string formatCake = $"{name};{price}" + "\n";

            content += formatCake;

            System.IO.File.WriteAllText("./Application/Resourses/database.csv", content);

            return new ViewResponse(HttpStatusCode.Ok, new UpdatedCake(name, price));
        }
        
        public IHttpResponse Search(IHttpRequest request)
        {
            if (!CheckIfLoggedIn(request))
                return new RedirectResponse("/login");

            return new ViewResponse(HttpStatusCode.Ok, new Search());
        }

        public IHttpResponse SearchCake(string searchCake)
        {
            return new ViewResponse(HttpStatusCode.Ok, new SearchCake(searchCake));
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
