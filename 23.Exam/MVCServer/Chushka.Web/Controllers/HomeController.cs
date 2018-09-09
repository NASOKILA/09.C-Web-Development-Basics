namespace Chushka.Web.Controllers
{
    using Chushka.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
                


            



            if (this.User.IsAuthenticated)
            {


                //check if user is admin
                if (userIsAdmin())
                {
                    this.Model["isAdmin"] = "block";
                }
                else
                {
                    this.Model["isAdmin"] = "none";
                }




                StringBuilder sb = new StringBuilder();
                List<Product> products = this.Context.Products.ToList();



                if (products.Count < 1)
                    sb.AppendLine(@"<div class=""col-12 text-center""><h1>No Products in the Database !</h1></div>");
               
                
                int count = 0;

                foreach (Product product in products)
                {
                    count++;

                    if (count == 1) {
                        sb.AppendLine(@"<div class=""row d-flex justify-content-around mt-4"">");
                    }

                    string descriptionToPass = product.Description
                        .Substring(0, Math.Min(50, product.Description.Length)) + ". . .";
                    

                    sb.AppendLine($@"
                    <a href=""/product/details?id={product.Id}"" class=""col-md-2"">
                    <div class=""product p-1 chushka-bg-color rounded-top rounded-bottom"">
                        <h5 class=""text-center mt-3"">{product.Name}</h5>
                        <hr class=""hr-1 bg-white"" />
                        <p class=""text-white text-center"">
                            {descriptionToPass}
                        </p>
                        <hr class=""hr-1 bg-white"" />
                        <h6 class=""text-center text-white mb-3"">${product.Price}</h6>
                    </div>
                    </a>");


                    /*
                      <a href="/products/details" class="col-md-2">
                    <div class="product p-1 chushka-bg-color rounded-top rounded-bottom">
                        <h5 class="text-center mt-3">Injektoplqktor</h5>
                        <hr class="hr-1 bg-white"/>
                        <p class="text-white text-center">
                            A universal tool for basically everything. It's ba...
                        </p>
                        <hr class="hr-1 bg-white"/>
                        <h6 class="text-center text-white mb-3">$1.56</h6>
                    </div>
                </a>
                     */

                    if (count == 5 || count == products.Count)
                    {
                        count = 0;
                        sb.AppendLine($@"</div>");
                    }

                }

                this.Model["result"] = sb.ToString().Trim();
                this.Model["username"] = this.User.Name;
                return View();
            }




            this.Model["isAdmin"] = "none";
            return View();
        }

        private bool userIsAdmin()
        {
            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            this.Model["displayAnonymous"] = this.User.IsAuthenticated ? "none" : "block";

            if (currentUser.RoleId == 1)
            {
                return true;
            }

            return false;
        }
    }
}
