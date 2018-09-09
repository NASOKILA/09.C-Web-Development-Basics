using Handmade_HTTP_Server.Server.Common;
using System;

namespace Handmade_HTTP_Server.Server.HTTP
{

    public class HttpHeader
    {
        //This class is responsible for keeping info about all the requests and responses

        public HttpHeader(string key, string value)
        {
            
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }
        
        public override string ToString()
        {
            return this.Key + ": " + this.Value;
        }
    }
}
