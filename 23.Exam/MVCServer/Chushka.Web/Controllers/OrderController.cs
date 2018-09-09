namespace Chushka.Web.Controllers
{
    using Chushka.Models;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OrderController : BaseController
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
        public IActionResult All()
        {

            if (!this.User.IsAuthenticated)
                return RedirectToAction("/user/login");

            if (userIsAdmin())
            {
                this.Model["isAdmin"] = "block";
            }
            else
            {
                this.Model["isAdmin"] = "none";
                return RedirectToAction("/");
            }


            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            var allOrders = this.Context.Orders
                .Include(u => u.Client)
                .Include(u => u.Product);
                
            int count = 1;
            string result = "";
            foreach (Order order in allOrders)
            {

                result += $@"<tr>
                                <th scope=""row"">{count}</th>
                                <td> {order.Id}</td>
                                <td> {order.Client.Username}</td>
                                <td> {order.Product.Name}</td>
                                <td> {order.OrderedOn.ToLocalTime()}</td>
                            </tr>";
                count++;

            }

            this.Model["table"] = result;
            return View();
        }

    }
}
