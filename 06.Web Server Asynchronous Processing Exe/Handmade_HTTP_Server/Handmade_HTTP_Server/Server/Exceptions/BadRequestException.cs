namespace Handmade_HTTP_Server.Server.HTTP
{
    using System;

    public class BadRequestException : Exception
    {
        public BadRequestException()
               : base("Request is not valid.")
        { }
    }
}