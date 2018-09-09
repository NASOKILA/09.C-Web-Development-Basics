namespace SimpleMvc.App.Controllers
{
    using SimpleMvs.Framework.Attributes.Methods;
    using SimpleMvs.Framework.Controllers;
    using SimpleMvs.Framework.Interfaces;

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            if (this.User.IsAuthenticated)
            {
                this.Model["displayType"] = "block";
                this.Model["loginRegisterDisplayType"] = "none";
            }
            else
            {
                this.Model["displayType"] = "none";
                this.Model["loginRegisterDisplayType"] = "block";

            }

            return this.View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return this.View();
        }
        
    }
}

