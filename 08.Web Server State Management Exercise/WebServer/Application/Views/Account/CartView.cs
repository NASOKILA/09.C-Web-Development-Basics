namespace HTTPServer.Application.Views.Account
{
    using HTTPServer.Server.Contracts;
    using System;
    using System.Linq;

    public class CartView : IView
    {
        public string View()
        {
            string[] products = System.IO.File.ReadAllText("./Application/Resourses/products.csv")
                    .Split("\n", StringSplitOptions.RemoveEmptyEntries).ToArray();

            string productsToString = "";
            double totalCost = 0;

            foreach (var product in products)
            {
                
                productsToString += product + "<br/>";

                double productCost = double.Parse(product.Replace(".", ",").Split(" - $", StringSplitOptions.RemoveEmptyEntries).ToArray()[1]);
                totalCost += productCost;
            }

            var result = System.IO.File.ReadAllText("./Application/Resourses/cart.html");
            string totalCostString = "$" + totalCost.ToString("f2");

            result = result.Replace(
                "<div id=\"orderedCakes\">No Items in cart</div>",
                $"<p>{productsToString}</p>");

            result = result.Replace(
                "<div id=\"totalCost\">Total Cost: $0</div>",
                $"<p>Total Cost: {totalCostString}</p>");


            return result;
        }
    }
}
