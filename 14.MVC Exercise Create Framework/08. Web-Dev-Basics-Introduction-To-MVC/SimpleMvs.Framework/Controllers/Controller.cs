namespace SimpleMvs.Framework.Controllers
{
    using Interfaces;
    using Interfaces.Generic;
    using System.Runtime.CompilerServices;
    using ViewEngine;
    using ViewEngine.Generic;


    public class Controller
    {
        protected IActionResult View([CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controllerName.Remove(controllerName.Length - 10),
                caller
                );

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult View(string controller, string action)
        {
         
            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controller,
                action
                );

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<T> View<T>(T model, string controller, string action)
        {

            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controller,
                action
                );

            return new ActionResult<T>(fullQualifiedName, model);
        }

        protected IActionResult<T> View<T>(T model, [CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controllerName.Remove(controllerName.Length - 10),
                caller
                );

            return new ActionResult<T>(fullQualifiedName, model);
        }
    }
}
