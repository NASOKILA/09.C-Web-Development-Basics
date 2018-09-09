namespace Chushka.Web.Controllers
{
    using Chushka.Data;
    using SimpleMvc.Framework.Controllers;
    
    public abstract class BaseController : Controller
    {

        protected ChushkaDbContext Context { get; set; }

        public BaseController()
        {
            this.Context = new ChushkaDbContext();
        }
    }
}
