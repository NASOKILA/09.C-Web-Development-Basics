namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using Data;
    using Framework.Attributes.Methods;
    using Framework.Controllers;
    using Framework.Contracts;
    using System.Linq;
    using System.Text;

    public class NotesController : Controller
    {

        private NotesDbContext Context { get; set;}

        public NotesController()
            :this(new NotesDbContext())
        {}

        public NotesController(NotesDbContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //we take only the id title and content of each note
            var notes = this.Context.Notes
                .Select(note => new NotesBindingModel()
                {
                    Title = note.Title,
                    Content = note.Content
                })
                .ToList();

            var sb = new StringBuilder();
            foreach (var note in notes)
            {
                sb.AppendLine($@"<div>
                    <div class=""card-header""><h5 class=""card-title"">{note.Title}</h5></div>
                    <div class=""card-body""><p class=""card-text"">{note.Content}</p></div>
                </div><br/>");
            }


            //we render it to notes in the view
            this.ViewModel["notes"] = sb.ToString();
            return this.View();
        }
    }
}
