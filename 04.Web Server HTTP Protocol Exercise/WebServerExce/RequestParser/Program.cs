namespace RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            List<string> paths = new List<string>();

            string response = "{0} {1} {2}\nContent-Length: {3}\nContent-Type: text/plain\n\n{4}";

            Dictionary<string, string> codes = new Dictionary<string, string>()
            {
                { "OK", "200" },
                { "NotFound", "404"}
            };

            string statusText = codes.Keys.FirstOrDefault();
            string statusCode = codes.Values.FirstOrDefault();
            

            string input = "";
            while ((input = Console.ReadLine()) != "END")
            {
                paths.Add(input);
            }

            
            string[] requestTokens = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string requestMethod = requestTokens[0];
            string requestPath = requestTokens[1];
            string request = requestTokens[2];
            

            foreach (var item in paths)
            {
                string[] pathTokens = item.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                
                string path = "/" + pathTokens[0];
                string method = pathTokens[1].ToUpper();


                if (requestPath == path && requestMethod == method)
                {
                    //200 OK
                    Console.WriteLine(string.Format(
                        response,
                        request,
                        statusCode,
                        statusText,
                        statusText.Length,
                        statusText));
                    
                    return;
                }
            }

            //404 NOT FOUND
            statusText = codes.Keys.LastOrDefault();
            statusCode = codes.Values.LastOrDefault();

            Console.WriteLine(string.Format(
                        response,
                        request,
                        statusCode,
                        statusText,
                        statusText.Length,
                        statusText));
            
        }
    }
}
