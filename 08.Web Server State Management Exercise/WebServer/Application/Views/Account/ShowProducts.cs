namespace HTTPServer.Application.Views.Account
{
    using HTTPServer.Server.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShowProducts : IView
    {
     
        public string View()
        {
            string[] products = System.IO.File.ReadAllText("./Application/Resourses/products.csv")
                    .Split("\n", StringSplitOptions.RemoveEmptyEntries).ToArray();

            int productsCount = products.Length;
            var result = System.IO.File.ReadAllText("./Application/Resourses/search.html");

            result = result.Replace(
                "<div id=\"products\"></div>",
                $"<span>{productsCount} products</span>");
            
            return result;
        }
    }
}
