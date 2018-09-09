namespace Handmade_HTTP_Server.Server.HTTP.Contracts
{
    using Server.Enums;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        HttpHeaderCollection Headers { get; }   
    }
}
