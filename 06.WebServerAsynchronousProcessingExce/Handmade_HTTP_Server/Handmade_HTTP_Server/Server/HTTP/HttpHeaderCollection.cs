namespace Handmade_HTTP_Server.Server.HTTP
{
    using Handmade_HTTP_Server.Server.Common;
    using Handmade_HTTP_Server.Server.HTTP.Contracts;
    using System;
    using System.Collections.Generic;

    public class HttpHeaderCollection : IHttpHeaderCollector
    {

        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            this.headers[header.Key] = header;
        }

        public bool ContrainsKey(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (!this.headers.ContainsKey(key)) {
                throw new InvalidOperationException($"The given key {key} is not present.");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers);
        }

    }
}



