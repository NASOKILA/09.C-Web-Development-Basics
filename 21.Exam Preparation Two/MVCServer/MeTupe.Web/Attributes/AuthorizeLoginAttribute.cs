namespace MeTupe.Web.Attributes
{
    using SimpleMvc.Framework.Attributes.Security;
    using System;
    using WebServer.Http.Contracts;
    
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeLoginAttribute : PreAuthorizeAttribute
    {
        public override IHttpResponse GetResponse(string message)
        {
            return base.GetResponse(@"/user/login");
        }
    }
}
