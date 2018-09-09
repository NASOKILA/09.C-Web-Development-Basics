namespace UrlDecode
{
    using System;
    using System.Net;

    public class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            string decodedUrl = WebUtility.UrlDecode(input);

            Console.WriteLine(decodedUrl);
        }
    }
}
