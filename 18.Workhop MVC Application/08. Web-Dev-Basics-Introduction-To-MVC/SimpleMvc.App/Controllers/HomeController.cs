namespace SimpleMvc.App.Controllers
{
    using Framework.Attributes.Methods;
    using Framework.Contracts;
    using Framework.Controllers;
    using System.Runtime.CompilerServices;

    public class HomeController : Controller
    {

        protected override IViewable View([CallerMemberName] string caller = "")
        {
            this.ViewModel["displayLogoutType"] =
                this.User.IsAuthenticated ? "block" : "none";

            this.ViewModel["displayRegister"] =
                this.User.IsAuthenticated ? "none" : "block";

            return base.View(caller);
        }

        [HttpGet]
        public IActionResult Index()
        { 
            return this.View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return this.View();
        }
        
    }
}

