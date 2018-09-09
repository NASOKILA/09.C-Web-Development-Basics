namespace Handmade_HTTP_Server.Server.HTTP
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using Enums;
    using Common;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {

        public HttpRequest(string requestText)
        {
            
            this.FormData = new Dictionary<string, string>();
            this.QueryParamerers = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestText);
        }
        
        public Dictionary<string, string> FormData { get; }

        public HttpHeaderCollection Headers { get; }

        public string Path { get; private set; }

        public Dictionary<string, string> QueryParamerers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, string> UrlParameters { get; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            //ako poluchim 2 parametura edni i sushti gi prezapisvame
            this.UrlParameters[key] = value;
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }

            this.ParseQuery(this.Url, this.UrlParameters);

        }

        private void ParseRequest(string requestString)
        {
            //Split the string

            string[] requestLines = requestString.Split(Environment.NewLine);
            
            if (!requestLines.Any())
                throw new BadRequestException();

            string[] requestLine = requestLines[0].Trim()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
                throw new BadRequestException();
            

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());

            this.Url = requestLine[1];

            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);

            this.ParseParameters();

            this.ParseFormData(requestLines.Last());



        }

        
        private HttpRequestMethod ParseRequestMethod(string method)
        {
            //parsvame enum

            try
            {
                HttpRequestMethod parseMethod;

                if (!Enum.TryParse<HttpRequestMethod>(method, true, out parseMethod))
                {
                    throw new BadRequestException();
                }

                return parseMethod;
            }
            catch
            {
                throw new BadRequestException();
            }
        }


        private void ParseHeaders(string[] requestLines)
        {
            int emptyLineAfterHeadersIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < emptyLineAfterHeadersIndex; i++)
            {
                var currentLine = requestLines[i];

                string[] headerParts = currentLine
                    .Split(new[] { ": " }, StringSplitOptions.None);

                if (headerParts.Length != 2){
                    throw new BadRequestException();
                }

                string key = headerParts[0];
                string value = headerParts[1].Trim();

                this.Headers.Add(new HttpHeader(key, value));
            }

            if (!Headers.ContrainsKey("Host"))
                throw new BadRequestException();
        }


        private void ParseFormData(string formDataLine)
        {
            if (this.RequestMethod == HttpRequestMethod.Get)
                return;

            this.ParseQuery(formDataLine, this.QueryParamerers);
        }

        private void ParseQuery(string queryString, IDictionary<string, string> dict)
        {
            var query = queryString
               .Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)
               .Last();

            if (!query.Contains('='))
            {
                return;
            }

            var queryPairs = query
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (queryKvp.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryKvp[0]);

                var queryValue = WebUtility.UrlDecode(queryKvp[1]);

                dict.Add(queryKey, queryValue);
            }
        }
        

    }
}
