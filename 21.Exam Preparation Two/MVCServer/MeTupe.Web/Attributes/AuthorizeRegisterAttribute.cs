namespace MeTupe.Web.Attributes
{
    using SimpleMvc.Framework.Attributes.Security;
    using WebServer.Http.Contracts;

    public class AuthorizeRegisterAttribute : PreAuthorizeAttribute
    {
        public override IHttpResponse GetResponse(string message)
        {
            return base.GetResponse(@"/user/register");
        }
    }
}
