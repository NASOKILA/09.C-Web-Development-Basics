namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using Data;
    using Models;
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using HTTPServer.Server.Http;

    public class ShoppingController : Controller
    {
        private readonly CakesData cakesData;

        public ShoppingController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var cake = this.cakesData.Find(id);

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);


            bool cartContainsProduct = shoppingCart
                .Orders
                .Any(i => i.Id == cake.Id && i.Name == cake.Name && i.Price == cake.Price);

            if (!cartContainsProduct){    
                shoppingCart.Orders.Add(cake);
            }



            var redirectUrl = "/search";
            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.Orders.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {

                
                List<int> itemsIds = shoppingCart
                    .Orders
                    .Select(i => i.Id).ToList();

                //durpame produktite ot bazata i gi slagame tuka
                List<Product> itemsInCart = new List<Product>();
                
                foreach (int id in itemsIds)
                {
                    using (var context = new ByTheCakeContext())
                    {
                        Product item = context.Products.Find(id);
                        itemsInCart.Add(item);
                    }
                }
                
                var items = itemsInCart
                    .Select(i => $"{i.Name} - ${i.Price:F2}<br />");

                var totalPrice = itemsInCart
                    .Sum(i => i.Price);
                
                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            //Register order in the database


            var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);

            //purvo slagame ordera
            using (var context = new ByTheCakeContext())
            {
                User currentUser = context.Users.Find(currentUserId);
                var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

                List<int> itemsIds = shoppingCart
                      .Orders
                      .Select(i => i.Id).ToList();

                List<Product> productItems = new List<Product>();

                foreach (var id in itemsIds)
                {
                    Product product = context.Products.Find(id);

                    productItems.Add(product);
                }
              

                //Suzdavam nov order
                Order order = new Order
                {
                    DateOfCreation = DateTime.UtcNow,
                    UserId = currentUserId
                    
                };
                
                context.Orders.Add(order);
                
                //za vseki produkt v karta suzdavam nov ProductOrder
                foreach (int id in itemsIds)
                {
                    Product item = context.Products.Find(id);
                    
                    ProductOrder productOrder = new ProductOrder
                    {
                        Order = order,
                        Product = item
                    };

                    context.ProductOrders.Add(productOrder);
                    
                }

                context.SaveChanges(); 
                
            }




            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }
    }
}
