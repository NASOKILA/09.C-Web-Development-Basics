namespace SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");

            int port = 1300;

            TcpListener server = new TcpListener(address, port);

            server.Start();
            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1: {port}");

            Task.Run(async () => await Connect(server)).Wait();
        }

        private static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
       
                byte[] request = new byte[1024];
                
                await client.GetStream().ReadAsync(request, 0 , request.Length);

                Console.WriteLine(Encoding.UTF8.GetString(request));

                var data = Encoding.UTF8.GetBytes("Hello From Server!");

                await client.GetStream().WriteAsync(data, 0, data.Length);

                Console.WriteLine("Closing connection.");
                client.Dispose();
            }
        }
    }
}
