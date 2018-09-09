namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using Data;
    using HTTPServer.Server.Exceptions;
    using HTTPServer.Server.Http.Response;
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CakesController : Controller
    {
        private readonly CakesData cakesData;

        public CakesController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Add(IHttpRequest req)
        {
            string name = req.FormData["name"];
            decimal price = decimal.Parse(req.FormData["price"]);
            string imageUrl = req.FormData["imageUrl"];
            
            var product = new Product
            {
                Name = name,
                Price = price,
                ImageUrl = imageUrl
            };

            //Dobvqme produkta v bazata
            using (var context = new ByTheCakeContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }

            this.ViewData["name"] = name;
            this.ViewData["price"] = price.ToString("f2");
            this.ViewData["imageUrl"] = imageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                this.ViewData["searchTerm"] = searchTerm;

                IEnumerable<string> cakeResults = null;

                using (var context = new ByTheCakeContext())
                {
                    cakeResults = context.Products
                        .Where(c => c.Name.ToLower().Contains(searchTerm))
                        .Select(c => $@"<div><a class=""btn btn-lg btn-light"" href=""/cakeDetails/{c.Id}"">{c.Name}</a> - ${c.Price:F2} <a class=""btn btn-lg btn-success"" href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div><br/>")
                        .ToList();

                    if (searchTerm == "*")
                    {
                        cakeResults = context.Products
                        .Select(c => $@"<div><a class=""btn btn-lg btn-light"" href=""/cakeDetails/{c.Id}"">{c.Name}</a> - ${c.Price:F2} <a class=""btn btn-lg btn-success"" href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div><br/>")
                        .ToList();
                    }
                }

                
                var results = "No cakes found";

                //Ako imame namereni torti gi subirame s nov red
                if (cakeResults.Any())
                {
                    results = string.Join(Environment.NewLine, cakeResults);
                }

                this.ViewData["results"] = results;
            }
            else
            {
                this.ViewData["results"] = "Please, enter search term";
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"cakes\search");
        }

        public IHttpResponse CakeDetails(IHttpRequest req)
        {

            int cakeId = int.Parse(req.UrlParameters["id"]);

            Product product = null;

            using (var context = new ByTheCakeContext())
            {
                product = context.Products
                    .FirstOrDefault(c => c.Id == cakeId);
            }

            if (product == null){
                return new BadRequestResponse();
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["imageUrl"] = product.ImageUrl;


            return this.FileViewResponse(@"cakes\details");
        }

        public IHttpResponse OrderDetails(IHttpRequest req)
        {

            int orderId = int.Parse(req.UrlParameters["id"]);

            decimal orderSum = 0;

            string result = "";

            Order order = null;

            using (var context = new ByTheCakeContext())
            {
                order = context.Orders
                    .FirstOrDefault(c => c.Id == orderId);

                if (order == null){
                    return new BadRequestResponse();
                }

                //Selektirame idtata na productite ot tozi order
                List<int> productIds = context.ProductOrders
                    .Where(p => p.ProductId == orderId)
                    .Select(p => p.OrderId).ToList();

                //vzimame vsichki produkti
                List<Product> products = context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToList();

                for(int i=0;i<products.Count;i++)
                {
                    var product = products[i];
                    orderSum += product.Price;
                    int count = i + 1;
                    result += $@"<tr><th scope=""row"">{count}</th><td><a href=""/cakeDetails/{product.Id}"">{product.Name}</a></td><td>${product.Price.ToString("F2")}</td></tr>";
                }
            }
            

             this.ViewData["id"] = order.Id.ToString();
            this.ViewData["result"] = result;
            this.ViewData["orderSum"] = orderSum.ToString("F2");
            this.ViewData["creationDate"] = order.DateOfCreation.ToString("dd-MM-yyyy");

            return this.FileViewResponse(@"cakes\orderDetails");
        }

    }
}
