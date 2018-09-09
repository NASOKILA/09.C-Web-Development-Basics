namespace Chushka.Web.Controllers
{
    using Chushka.Models;
    using BindingModels;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class ProductController : BaseController
    {
        private bool userIsAdmin()
        {
            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            this.Model["displayAnonymous"] = this.User.IsAuthenticated ? "none" : "block";

            if (!this.User.IsAuthenticated)
            {
                return false;
            }

            if (currentUser.RoleId == 1)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public IActionResult Details()
        {

            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");


            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
            }


            int id = int.Parse(this.Request.UrlParameters["id"]);

            Product product = this.Context.Products.FirstOrDefault(t => t.Id == id);


            Models.Type type = this.Context.Types.FirstOrDefault(t => t.Id == product.TypeId);

            this.Model["name"] = product.Name.ToString();
            this.Model["id"] = product.Id.ToString();
            this.Model["type"] = type.Name;
            this.Model["price"] = product.Price.ToString();
            this.Model["description"] = product.Description.ToString();
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {

            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");


            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
                return RedirectToAction("/");

            }



            this.Model["error"] = "";
            return this.View();
        }


        [HttpPost]
        public IActionResult Create(ProductBindingModel productBindingModel)
        {

            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");


            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
            }



            if (productBindingModel.Name == ""
                || productBindingModel.Price == ""
                || productBindingModel.Type == "")
            {
                this.Model["error"] = "Empty input fields !";
                return View();
            }

            if (!this.IsValidModel(productBindingModel))
            {
                this.Model["error"] = "Something went wrong !";
                return View();
            }

            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            Models.Type type = this.Context.Types.FirstOrDefault(t => t.Name == productBindingModel.Type);

            if (type == null)
            {
                this.Model["error"] = "The Type is invalid!";
                return View();
            }

            Product product = new Product()
            {
                TypeId = type.Id,
                Description = productBindingModel.Description,
                Name = productBindingModel.Name,
                Orders = new List<Order>(),
                Price = decimal.Parse(productBindingModel.Price)
            };

            this.Context.Products.Add(product);
            this.Context.SaveChanges();

            return this.RedirectToAction("/");

        }


        [HttpGet]
        public IActionResult Order()
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToAction("/user/login");
            }

            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
            }


            int id = int.Parse(this.Request.UrlParameters["id"]);

            Product product = this.Context.Products.FirstOrDefault(t => t.Id == id);

            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            Order order = new Order()
            {
                ClientId = currentUser.Id,
                ProductId = product.Id,
                OrderedOn = DateTime.Now
            };

            this.Context.Orders.Add(order);
            product.Orders.Add(order);
            this.Context.SaveChanges();


            return View();
        }


        [HttpGet]
        public IActionResult Edit()
        {
            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");

            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
                return RedirectToAction("/");

            }

            int id = int.Parse(this.Request.UrlParameters["id"]);

            Product product = this.Context.Products
                .Include(u => u.Type)
                .FirstOrDefault(p => p.Id == id);


            string radioButtonHtml = "";

            switch (product.Type.Name)
            {
                case "Food":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" checked value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Domestic":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" checked value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Health":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" checked value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Cosmetic":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" checked value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Other":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" checked value=""Other""> Other
                                </div> ";
                    break;

                default:
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
            }


            this.Model["name"] = product.Name;
            this.Model["price"] = product.Price.ToString();
            this.Model["description"] = product.Description;
            this.Model["radioButtonHtml"] = radioButtonHtml;
            this.Model["error"] = "";

            return this.View();

        }


        [HttpPost]
        public IActionResult Edit(ProductBindingModel productBindingModel)
        {


            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");


            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
            }




            int id = int.Parse(this.Request.UrlParameters["id"]);

            Product product = this.Context.Products.Find(id);

            Models.Type type = this.Context.Types.FirstOrDefault(t => t.Name == productBindingModel.Type);



            if (productBindingModel.Name == ""
                || productBindingModel.Price == ""
                || productBindingModel.Type == "")
            {

                string radioButtonHtml = "";
                switch (product.Type.Name)
                {
                    case "Food":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" checked value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Domestic":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" checked value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Health":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" checked value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Cosmetic":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" checked value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Other":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" checked value=""Other""> Other
                                </div> ";
                        break;

                    default:
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                }

                this.Model["name"] = product.Name;
                this.Model["price"] = product.Price.ToString();
                this.Model["description"] = product.Description;
                this.Model["radioButtonHtml"] = radioButtonHtml;
                this.Model["error"] = "Empty input fields !";
                return View();
            }

            if (!this.IsValidModel(productBindingModel))
            {
                string radioButtonHtml = "";
                switch (product.Type.Name)
                {
                    case "Food":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" checked value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Domestic":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" checked value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Health":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" checked value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Cosmetic":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" checked value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                    case "Other":
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" checked value=""Other""> Other
                                </div> ";
                        break;

                    default:
                        radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" name=""Type"" value=""Health""> Food
                                    <input type=""radio"" name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" name=""Type"" value=""Health""> Health
                                    <input type=""radio"" name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" name=""Type"" value=""Other""> Other
                                </div> ";
                        break;
                }

                this.Model["name"] = product.Name;
                this.Model["price"] = product.Price.ToString();
                this.Model["description"] = product.Description;
                this.Model["radioButtonHtml"] = radioButtonHtml;
                this.Model["error"] = "Empty input fields !";
                this.Model["error"] = "Something went wrong !";
                return View();
            }

            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);



            product.Name = productBindingModel.Name;
            product.Price = decimal.Parse(productBindingModel.Price);
            product.Description = productBindingModel.Description;
            product.TypeId = type.Id;

            this.Context.Products.Update(product);
            this.Context.SaveChanges();

            return this.RedirectToAction("/");

        }


        [HttpGet]
        public IActionResult Delete()
        {
            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");

            //check if user is admin
            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
                return RedirectToAction("/");

            }

            int id = int.Parse(this.Request.UrlParameters["id"]);

            Product product = this.Context.Products
                .Include(u => u.Type)
                .FirstOrDefault(p => p.Id == id);


            string radioButtonHtml = "";

            switch (product.Type.Name)
            {
                case "Food":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" disabled name=""Type"" checked value=""Health""> Food
                                    <input type=""radio"" disabled name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Health
                                    <input type=""radio"" disabled name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" disabled name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Domestic":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Food
                                    <input type=""radio"" disabled name=""Type"" checked value=""Domestic""> Domestic
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Health
                                    <input type=""radio"" disabled name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" disabled name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Health":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Food
                                    <input type=""radio"" disabled name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" disabled name=""Type"" checked value=""Health""> Health
                                    <input type=""radio"" disabled name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" disabled name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Cosmetic":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Food
                                    <input type=""radio"" disabled name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Health
                                    <input type=""radio"" disabled name=""Type"" checked value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" disabled name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
                case "Other":
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Food
                                    <input type=""radio"" disabled name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Health
                                    <input type=""radio"" disabled name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" disabled name=""Type"" checked value=""Other""> Other
                                </div> ";
                    break;

                default:
                    radioButtonHtml = @"
                                <div class=""form-group"">
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Food
                                    <input type=""radio"" disabled name=""Type"" value=""Domestic""> Domestic
                                    <input type=""radio"" disabled name=""Type"" value=""Health""> Health
                                    <input type=""radio"" disabled name=""Type"" value=""Cosmetic""> Cosmetic
                                    <input type=""radio"" disabled name=""Type"" value=""Other""> Other
                                </div> ";
                    break;
            }


            this.Model["name"] = product.Name;
            this.Model["id"] = product.Id.ToString();
            this.Model["price"] = product.Price.ToString();
            this.Model["description"] = product.Description;
            this.Model["radioButtonHtml"] = radioButtonHtml;
            this.Model["error"] = "";

            return this.View();

        }


        [HttpPost]
        public IActionResult DeletePost()
        {

            int id = int.Parse(this.Request.UrlParameters["id"]);

            Product product = this.Context.Products.Find(id);


            this.Context.Products.Remove(product);
            this.Context.SaveChanges();

            return RedirectToAction("/");
        }

    }

}
