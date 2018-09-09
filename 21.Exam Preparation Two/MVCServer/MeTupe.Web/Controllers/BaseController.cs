namespace MeTupe.Web.Controllers
{
    using MeTube.Data;
    using SimpleMvc.Framework.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BaseController : Controller
    {

        protected MeTubeDbContext Context { get; set; }

        public BaseController()
        {
            this.Context = new MeTubeDbContext();
        }
    }
}
