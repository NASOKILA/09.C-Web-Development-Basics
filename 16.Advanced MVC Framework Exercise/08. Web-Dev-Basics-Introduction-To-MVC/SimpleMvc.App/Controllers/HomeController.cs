namespace SimpleMvc.App.Controllers
{
    using SimpleMvs.Framework.Attributes.Methods;
    using SimpleMvs.Framework.Controllers;
    using SimpleMvs.Framework.Interfaces;

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() {

            return this.RedirectToAction("/home/about");
        }

        [HttpGet]
        public IActionResult About()
        {

            return this.View();
        }


    }

}

