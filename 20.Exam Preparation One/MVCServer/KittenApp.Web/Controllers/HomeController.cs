namespace KittenApp.Web.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            
            if (this.User.IsAuthenticated)
            {
                this.Model["message"] = $"Welcome {this.User.Name}!";
            }
            else
            {
                this.Model["message"] = $"Welcome to Fluffy Duffy Munchkin Cats";
            }

            return this.View();
        }
    }
}
