
namespace MeTupe.Web.Controllers
{
    using MeTybe.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {

            if (this.User.IsAuthenticated)
            {
                User currentUser = this.Context.Users
                    .FirstOrDefault(u => u.Username == this.User.Name);

                string result = @"<div class=""col-12 text-center""><h1>No tubes in DB !</h1></div>";

                var myTubes = this.Context.Tubes
                    .Where(t => t.UploaderId == currentUser.Id)
                    .ToList();

                int count = 0;

                if(myTubes.Count >= 1)
                    result = "";

                for (int i = 0; i < myTubes.Count; i++)
                {
                    
                    Tube currentTube = myTubes[i];

                    count++;
                    
                    result += $@"<div class=""col-4 text-center""> 
                                    <div>
                                        <iframe width=""250"" height=""175"" 
                                            src=""https://www.youtube.com/embed/{currentTube.YoutubeId}"" 
                                            frameborder = ""0"" allowfullscreen>
                                        </iframe >
                                    </div>

                                    <div class=""card-body"">
                                        <h4 class=""card-title"">{currentTube.Title}</h4>
                                        <p class=""card-text"">{currentTube.Author}</p>
                                        <br/>
                                    </div>
                                </div>";                    
                }
                
                this.Model["result"] = result;

                this.Model["username"] = this.User.Name;
            }

            return this.View();
        }

    }
}


/*                                <div>
                                    <iframe width=""160"" height=""85"" 
                                            src=""https://www.youtube.com/embed/{currentTube.YoutubeId}"" 
                                            frameborder = ""0"" allowfullscreen>
                                    </ iframe >
                                </div>
*/
