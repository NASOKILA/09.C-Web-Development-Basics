namespace Handmade_HTTP_Server.Server
{
    using Routing.Contracts;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using System;
    using HTTP;
    using Handmade_HTTP_Server.Server.Handlers;
    using System.Text;


    public class ConnectionHandler
    {
        //mojem da chetem i izprashtame baitove su Socket klasa
        private readonly Socket client;
        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync() {

            var request = await this.ReadRequest();

            if (request != null ) {

                var httpContext = new HttpContext(request);

                var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

                var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

                var byteSegments = new ArraySegment<byte>(responseBytes);

                await this.client.SendAsync(byteSegments, SocketFlags.None);

                Console.WriteLine($"-----REQUEST---");
                Console.WriteLine(request);
                Console.WriteLine($"-----RESPONSE---");
                Console.WriteLine(httpResponse);
                Console.WriteLine();
            }

            

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<HttpRequest> ReadRequest()
        {
            var result = new StringBuilder();

            var data = new ArraySegment<byte>(new byte[1024]);


            
            while (true)
            {
                int numberOfButesRead =
                    await this.client.ReceiveAsync(data, SocketFlags.None);

                if (numberOfButesRead < 1)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfButesRead);

                result.Append(bytesAsString);
                
                if (numberOfButesRead < 1023)
                {
                    break;
                }

            }

            if (result.Length == 0) {
                return null;
            }

            return new HttpRequest(result.ToString());
        }
    }

}
