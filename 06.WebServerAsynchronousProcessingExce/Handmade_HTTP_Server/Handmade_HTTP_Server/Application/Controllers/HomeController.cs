namespace Handmade_HTTP_Server.Application.Controllers
{
    using Application.Views.Home;
    using Server.HTTP.Response;
    using Server.HTTP.Contracts;
    using Server.Enums;

    public class HomeController
    {
        public IHttpResponse Index() 
        {
            return new ViewResponse(HttpStatusCode.OK, new IndexView());
        }
    }
}
