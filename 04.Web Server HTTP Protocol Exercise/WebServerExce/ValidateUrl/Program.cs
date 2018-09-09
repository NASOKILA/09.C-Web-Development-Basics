namespace ValidateUrl
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    public class Program
    {
        static void Main(string[] args)
        {
            
            string inputUrl = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(inputUrl);
            

            Regex protocolRegex = new Regex(@"\b(http)\b|\b(https)\b|\b(ftp)\b");
            Regex hostRegex = new Regex(@"(?<=\/\/)([a-zA-Z.]+)");
            Regex portRegex = new Regex(@"(?<=:)(\d+)");
            Regex pathRegex = new Regex(@"(\/\w+)(?=\?)|\/$|(\/\w+)$");
            Regex queryStringRegex = new Regex(@"(?<=\?)(.+)(?=#)|(?<=\?)([a-zA-Z=&]+)$");
            Regex fragmentRegex = new Regex(@"(?<=#)\w+$");


            string protocol = protocolRegex.Match(decodedUrl).ToString();
            string host = hostRegex.Match(decodedUrl).ToString();
            string port = portRegex.Match(decodedUrl).ToString();
            string path = pathRegex.Match(decodedUrl).ToString();
            string queryString = queryStringRegex.Match(decodedUrl).ToString();
            string fragment = fragmentRegex.Match(decodedUrl).ToString();

            
            if (port == "" && protocol == "http")
                port = "80";
            else if(port == "" && protocol == "https")
                port = "433";


            if (protocol == "http" && port != "80"){
                Console.WriteLine("Invalid URL"); 
                return;
            }
            else if (protocol == "https" && port != "443"){
                Console.WriteLine("Invalid URL");
                return;
            }


            Console.WriteLine(decodedUrl);
            Console.WriteLine("Protocol: " + protocol);
            Console.WriteLine("Host: " + host);
            Console.WriteLine("Port: " + port);
            Console.WriteLine("Path: " + path);

            if(queryString != "")
                Console.WriteLine("Query: " + queryString);

            if (fragment != "")
                Console.WriteLine("Fragment: " + fragment);


        }
    }
}
