namespace NotesApp.Controllers
{
    using NotesApp.Data;
    using SimpleMvc.Framework.Controllers;

    public abstract class BaseController : Controller
    {
        public BaseController()
            : this(new NotesAppContext())
        {
        }

        public BaseController(NotesAppContext context)
        {
            this.Context = context;
        }

        public NotesAppContext Context { get; set; }
    }
}
