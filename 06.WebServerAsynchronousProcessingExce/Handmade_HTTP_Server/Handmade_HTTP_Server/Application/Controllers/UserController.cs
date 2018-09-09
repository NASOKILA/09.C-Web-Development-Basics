namespace Handmade_HTTP_Server.Application.Controllers
{
    using Server.HTTP.Response;
    using Server.HTTP.Contracts;
    using Server.Enums;
    using Views.Register;
    using Server;
    using Application.Views.User;

    public class UserController
    {
        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new RegisterView());
        }

        public IHttpResponse RegisterPost(string name)
        {
            return new RedirectResponse($"/user/{name}");
        }

        public IHttpResponse Details(string name) {

            Model model = new Model { ["name"] = name };
            return new ViewResponse(HttpStatusCode.OK, new UserDetailsView(model));
        }
    }
}

