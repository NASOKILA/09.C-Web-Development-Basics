using KittenApp.Data;
using SimpleMvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KittenApp.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            this.Context = new KittenAppDbContext();
        }

        protected KittenAppDbContext Context { get; set; }
    }
}
