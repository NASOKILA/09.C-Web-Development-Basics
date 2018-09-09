namespace Handmade_HTTP_Server.Server.HTTP.Contracts
{
    using Handmade_HTTP_Server.Server.Enums;
    using System.Collections.Generic;
    
    public interface IHttpRequest
    {
        Dictionary<string, string> FormData { get; }

        HttpHeaderCollection Headers { get; }
            
        string Path { get; }
        
        Dictionary<string, string> QueryParamerers { get; }

        HttpRequestMethod RequestMethod { get; }

        string Url { get; }

        Dictionary<string, string> UrlParameters { get; }

        void AddUrlParameter(string key, string value); 
    }
}
