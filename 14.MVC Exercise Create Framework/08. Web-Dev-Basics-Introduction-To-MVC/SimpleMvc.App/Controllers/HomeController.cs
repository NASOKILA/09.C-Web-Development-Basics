
namespace SimpleMvc.App.Controllers
{

    using SimpleMvs.Framework.Controllers;
    using SimpleMvs.Framework.Interfaces;

    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
        }
        
    }
}
