namespace Handmade_HTTP_Server.Server.HTTP.Contracts
{
    
    interface IHttpHeaderCollector
    {
        void Add(HttpHeader header);

        bool ContrainsKey(string key);

        HttpHeader GetHeader(string key);
    }
}
