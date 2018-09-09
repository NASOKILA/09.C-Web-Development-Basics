namespace SimpleMvc.Framework.Attributes.Security
{
    using System;
    using WebServer.Http.Contracts;

    //we declare that we can use it on methods
    [AttributeUsage(AttributeTargets.Method)]
    public class PreAuthorizaAttribute : Attribute //inherits class Attribute
    {
    }
}
