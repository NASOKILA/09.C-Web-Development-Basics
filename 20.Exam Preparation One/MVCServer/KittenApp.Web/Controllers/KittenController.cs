namespace KittenApp.Web.Controllers
{
    using KittenApp.Models;
    using KittenApp.Web.BindingModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using System.Text;

    public class KittenController : BaseController
    {
        [HttpGet]
        public IActionResult All()
        {
            
            using (this.Context)
            {
                var cats = Context.Kittens.ToList();

                StringBuilder sb = new StringBuilder();

                if (cats.Count < 1)
                {
                    this.Model["all"] = "<h3>No Cats in DB !</h3>";
                    return this.View();
                }

                sb.AppendLine("<ul class=\"list-group\">");

                foreach (var cat in cats)
                {
                    var breed = this.Context.Breads.FirstOrDefault(u => u.Id == cat.BreedId);
                    
                    string imageUrl = string.Empty;
                    imageUrl = setImage(breed);

                    sb.AppendLine($"<li class=\"list-group-item\">" +
                        $"<img height=\"200\" width=\"200\" src=\"{imageUrl}\"/>" +
                        $"|<b>Name:</b> {cat.Name} |" +
                        $"<b>Age:</b> {cat.Age} |" +
                        $"<b>Breed:</b> {breed.Name}" +
                         $"</li>");
                }

                sb.AppendLine("</ul>");

                this.Model["all"] = sb.ToString().Trim();
                return this.View();
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            this.Model["error"] = "";
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddKittenBindingModel addKittenBindingModel)
        {

            if (!this.IsValidModel(addKittenBindingModel))
            {
                this.Model["error"] = "<h1 class=\"message\">Something went wrong !</h1>";
                return View();
            }
            
            using (this.Context)
            {
                var currendbBreed = Context.Breads.FirstOrDefault(b => b.Name == addKittenBindingModel.Breed);

                if (currendbBreed == null){

                    this.Model["error"] = "<h1 class=\"message\">Breed is not valid!</h1>";
                    return View();
                }

                var kitten = new Kitten()
                {
                    Name = addKittenBindingModel.Name,
                    Age = addKittenBindingModel.Age,
                    Breed = currendbBreed,
                    BreedId = currendbBreed.Id
                };

                currendbBreed.Kittens.Add(kitten);

                Context.Kittens.Add(kitten);

                Context.SaveChanges();
                
                return RedirectToAction("/kitten/all");
            }

        }

        private static string setImage(Breed breed)
        {
            string imageUrl;
            switch (breed.Name)
            {
                case "Street Transcended":
                    imageUrl = "https://www.pawboost.com/blog/wp-content/uploads/2018/03/Katrina-Tampa.jpeg";
                    break;
                case "American Shorthair":
                    imageUrl = "http://cdn3-www.cattime.com/assets/uploads/2011/12/file_2716_American-Shorthair-cat-bree-460x290.jpg";
                    break;
                case "Munchkin":
                    imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTW1bojSZ6IakLHcp1jEAgSVAuYV9yeOt07fACayHIdO2M6grBt5A";
                    break;
                case "Siamese":
                    imageUrl = "https://vetstreet.brightspotcdn.com/dims4/default/b73de6d/2147483647/thumbnail/645x380/quality/90/?url=https%3A%2F%2Fvetstreet-brightspot.s3.amazonaws.com%2Fe6%2F75%2F714c36604fc0aefcc5df187a06b9%2FSiamese-AP-U2BPOE-645sm3614.jpg";
                    break;
                default:
                    imageUrl = "";
                    break;
            }

            return imageUrl;
        }

    }
}
