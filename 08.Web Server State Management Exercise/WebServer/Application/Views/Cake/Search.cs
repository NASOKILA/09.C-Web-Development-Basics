using HTTPServer.Server.Contracts;
using System;
using System.Linq;

namespace HTTPServer.Application.Views.Cake
{
    public class Search : IView
    {
        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/search.html");

            string[] products = System.IO.File.ReadAllText("./Application/Resourses/products.csv")
                    .Split("\n", StringSplitOptions.RemoveEmptyEntries).ToArray();

            int productCount = products.Length;

            if(productCount == 1)
                result = result.Replace("<a href=\"/cart\">Your Cart</a>", "<a href=\"/cart\">Your Cart</a>" + " " + productCount + "  product" );
            else
                result = result.Replace("<a href=\"/cart\">Your Cart</a>", "<a href=\"/cart\">Your Cart</a>" + " " + productCount + "  products");

            return result;

        }
    }
}
