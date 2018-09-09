namespace MeTupe.Web.Controllers
{
    using MeTupe.Web.Attributes;
    using MeTupe.Web.BindingModels;
    using MeTybe.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public class TubeController : BaseController
    {

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Add()
        {
            this.Model["error"] = "";

            return this.View();
        }
 
        [HttpPost]
        [AuthorizeLogin]
        public IActionResult Add(RegisterTubeBindingModel registerTubeBindingModel)
        {
            
            if (registerTubeBindingModel.Title == ""
                || registerTubeBindingModel.Author == ""
                || registerTubeBindingModel.YoutubeId == "")
            {
                this.Model["error"] = "Empty input fields !";
                return View();
            }

            if (!this.IsValidModel(registerTubeBindingModel))
            {
                this.Model["error"] = "Something went wrong !";
                return View();
            }    

            User currentUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);

            Tube tube = new Tube()
            {
                Author = registerTubeBindingModel.Author,
                Title = registerTubeBindingModel.Title,
                YoutubeId = registerTubeBindingModel.YoutubeId,
                Description = registerTubeBindingModel.Description,
                UploaderId = currentUser.Id,
                Views = 0,
            };

            this.Context.Tubes.Add(tube);
            this.Context.SaveChanges();
            
            return this.RedirectToAction("/");
        }

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Details()
        {
            int id = int.Parse(this.Request.UrlParameters["id"]);

            Tube tube = this.Context.Tubes.FirstOrDefault(t => t.Id == id);

            tube.Views++;
            this.Context.Update(tube);
            this.Context.SaveChanges();

            this.Model["title"] = tube.Title;
            this.Model["author"] = tube.Author;
            this.Model["views"] = tube.Views.ToString();
            this.Model["youtubeId"] = tube.YoutubeId;
            this.Model["description"] = tube.Description;

            return this.View();
        }
    }
}
