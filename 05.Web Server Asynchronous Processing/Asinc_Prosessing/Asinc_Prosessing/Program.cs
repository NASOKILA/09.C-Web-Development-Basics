using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Asinc_Prosessing
{
    class Program
    {

        static void LongOperation()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            
            Console.WriteLine("First");
            
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++){
                    Console.WriteLine(i);
                }
            });


            thread.Start();

            for (int i = 1000; i < 1010; i++)
            {
                Console.WriteLine(i);
            }

            
            thread.Join();

            Console.WriteLine("Second");

            Thread LongOperationThread = new Thread(() =>
            {
                LongOperation();
            });

            Console.WriteLine();
            Console.WriteLine();

            List<int> list = Enumerable.Range(1, 10000).ToList();

            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    lock (list)
                    {
                        while (list.Count > 0)
                        {
                            list.Remove(list.Count - 1);
                        }
                    }
                    
                }).Start();
            }
        }

        static private async Task TCP_Listener() {

            int port = 8000;
            IPAddress ipAddress = IPAddress.Parse("localhost:");
            TcpListener server = new TcpListener(ipAddress, port);
            server.Start();
        }
    }
}
