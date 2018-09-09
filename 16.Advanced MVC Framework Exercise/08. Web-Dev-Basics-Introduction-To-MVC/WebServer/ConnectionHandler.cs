namespace WebServer
{
    using System;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using Http;
    using Http.Contracts;
    using Http.Response;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IHandleable requestHandler;

        private readonly IHandleable resourceHandler;

        public ConnectionHandler(Socket client, IHandleable requestHandler, IHandleable resourceHandler)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(requestHandler, nameof(requestHandler));
            CoreValidator.ThrowIfNull(resourceHandler, nameof(resourceHandler));

            this.client = client;
            this.requestHandler = requestHandler;
            this.resourceHandler = resourceHandler;
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var httpResponse = await this.HandleRequest(httpRequest);

                var responseBytes = await this.GetResponseBytes(httpResponse);

                var byteSegments = new ArraySegment<byte>(responseBytes);

                await this.client.SendAsync(byteSegments, SocketFlags.None);

                Console.WriteLine($"-----REQUEST-----");
                Console.WriteLine(httpRequest);
                Console.WriteLine($"-----RESPONSE-----");
                Console.WriteLine(httpResponse);
                Console.WriteLine();
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<byte[]> GetResponseBytes(IHttpResponse response)
        {
            var responseBytes = Encoding.UTF8
                .GetBytes(response.ToString())
                .ToList();

            if (response is FileResponse)
            {
                responseBytes.AddRange(((FileResponse)response).FileData);
            }

            return responseBytes.ToArray();
        }

        private async Task<IHttpResponse> HandleRequest(IHttpRequest request)
        {
            if (request.Path.Contains("."))
            {
                return this.resourceHandler.Handle(request);
            }
            else
            {
                string sessionIdToSend = this.SetRequestSession(request);
                var result = this.requestHandler.Handle(request);

                this.SetResponseSession(result, sessionIdToSend);
                return result;
            }
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;
            if (httpRequest.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                //get the cookie and the sessionId
                var cookie = httpRequest.Cookies.Get(SessionStore.SessionCookieKey);
                sessionId = cookie.Value;

                //save the value of that id in the httpRequest Session
                httpRequest.Session = SessionStore.Get(sessionId);

            }
            else
            {
                //if we dont have that cookie
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = new HttpSession(sessionId);   
            }

             return sessionId;
            
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            //httpResponse.Cookies.Add(new HttpCookie(SessionStore.CurrentUserKey, sessionData));
            var session = SessionStore.Get(sessionId);

            //We set the session in the response header
            httpResponse.Headers.Add(HttpHeader.SetCookie, sessionId);


        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();
            
            var data = new ArraySegment<byte>(new byte[1024]);
            
            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);

                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }
            
            return new HttpRequest(result.ToString());
        }
    }
}
