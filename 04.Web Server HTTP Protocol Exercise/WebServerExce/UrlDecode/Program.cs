namespace UrlDecode
{
    using System;
    using System.Net;

    public class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            //WebUtility avtomatichno ni decodira i enkodira URL-i i HTML-i, 
            //moje da pravi i drugi neshta.
            string decodedUrl = WebUtility.UrlDecode(input);

            Console.WriteLine(decodedUrl);
        }
    }
}
