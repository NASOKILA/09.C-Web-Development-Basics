namespace HTTPServer.Application.Controllers
{
    using Views.Register;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Application.Views.Home;

    public class UserController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new IndexView());
        }

        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new RegisterView());
        }

        public IHttpResponse RegisterPost(string name)
        {
            return new RedirectResponse($"/user/{name}");
        }
        
    }
}
