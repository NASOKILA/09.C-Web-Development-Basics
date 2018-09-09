
namespace Handmade_HTTP_Server.Server
{
    using Contracts;
    using Routing;
    using Routing.Contracts;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using System;

    public class WebServer : IRunnable
    {

        private const string localHostIpAddress = "127.0.0.1";
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener listener;
        private bool isRunning;
        
        public WebServer(int port, IAppRouteConfig appRouteConfig)
        {
            this.port = port;
            this.listener = 
                new TcpListener(IPAddress.Parse(localHostIpAddress), port);

            this.serverRouteConfig = new ServerRouteConfig(appRouteConfig);
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server running on port {localHostIpAddress}:{this.port}.");



            //TUK HVURLQ GRESHKA PRI POST ZAQVKA AKO POLZVAME GOOGLE CHROME, NE ZNAM ZASHTO.
            //MINI NA FIREFOX I NQMA DA GURMI.
            Task task = Task.Run(this.ListenLoop);
            task.Wait();
        }
        
        private async Task ListenLoop() {

            while (this.isRunning)
            {
                Socket client = await this.listener.AcceptSocketAsync();
                ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);

                await connectionHandler.ProcessRequestAsync();
            }

        }

    }
}
