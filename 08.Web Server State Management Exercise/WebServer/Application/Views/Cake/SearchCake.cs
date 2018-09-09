namespace HTTPServer.Application.Views.Cake
{
    using HTTPServer.Server.Contracts;
    using System;
    using System.Linq;
    using System.Text;

    public class SearchCake : IView
    {

        public string searchCake { get; set; }

        public SearchCake(string searchCake)
        {
            this.searchCake = searchCake;
            View();
        }

        public string View()
        {
            
            var cakes = System.IO.File.ReadAllText("./Application/Resourses/database.csv")
                    .Replace(";", " ");

            string[] cakesArray = cakes.Split("\n", System.StringSplitOptions.RemoveEmptyEntries).ToArray();

            var result = new StringBuilder();   
            result.AppendLine("<p>");


            foreach (var curentCake in cakesArray)
            {

                result.AppendLine("<form name=\"order-form\" method=\"POST\" action=\"/order\">");

                string key = curentCake.Split(" ")[0];
                string value = curentCake.Split(" ")[1].Replace(",", ".");

                string order = key + " - $" + value;
                if (key.Contains(this.searchCake))
                {
                    result.AppendLine($"<input style=\"border: none\" type=\"text\" name=\"orderedCake\" value=\"{order}\" />" + "<input type=\"submit\" name=\"order\"  value=\"Order\"/>" + "<br/>");
                }
                result.AppendLine("</form>");
            }
            
            result.AppendLine("</p>");

            var resultText = System.IO.File.ReadAllText("./Application/Resourses/search.html");

            string[] products = System.IO.File.ReadAllText("./Application/Resourses/products.csv")
                    .Split("\n", StringSplitOptions.RemoveEmptyEntries).ToArray();

            int productCount = products.Length;



            if (productCount == 1)
                result = result.Replace("<a href=\"/cart\">Your Cart</a>", "<a href=\"/cart\">Your Cart</a>" + " " + productCount + "  product");
            else
                result = result.Replace("<a href=\"/cart\">Your Cart</a>", "<a href=\"/cart\">Your Cart</a>" + " " + productCount + "  products");


            resultText = resultText.Replace("<div id=\"replace\"></div>", result.ToString().Trim());
            
            return resultText;
        }
    }
}
