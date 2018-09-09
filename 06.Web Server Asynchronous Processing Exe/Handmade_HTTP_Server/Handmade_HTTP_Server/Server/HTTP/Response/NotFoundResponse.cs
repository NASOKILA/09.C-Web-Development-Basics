namespace Handmade_HTTP_Server.Server.HTTP.Response
{
    using Enums;
    
    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
