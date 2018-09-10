namespace SimpleMvc.Framework.Attributes.Security
{
    using System;
    using WebServer.Http.Contracts;

    [AttributeUsage(AttributeTargets.Method)]
    public class PreAuthorizaAttribute : Attribute 
    {
    }
}
