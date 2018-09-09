namespace DownloadFile
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {
            DownloadFile("https://www.suddenlycat.com/wp-content/uploads/2018/04/Young_cats.jpg", "Cat_2", ".jpeg");
        }

        static void DownloadFile(string url, string filename, string format) {

            Console.WriteLine("Downloading...");

            using (var client = new WebClient())
            {
                client.DownloadFile(url, filename + format);    

                Console.WriteLine("File downloaded !!!");                
            }
        }
    }
}
