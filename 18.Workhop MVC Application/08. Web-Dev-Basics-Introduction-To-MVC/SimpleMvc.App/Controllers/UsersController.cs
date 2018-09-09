namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using App.Utilities;
    using Data;
    using Domain;
    using Framework.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Attributes.Methods;
    using Framework.Contracts;
    using Framework.Security;
    using System.Runtime.CompilerServices;

    public class UsersController : Controller
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
        public IActionResult Register()
        {
            this.ViewModel["message"] = "<p></p>";
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {

            if (!this.IsValidModel(registerUserBindingModel)) {

                this.ViewModel["message"] = "<p>Something went wrong !</p>";
                return View();
            }

            if (registerUserBindingModel.Password == "" 
                || registerUserBindingModel.ConfirmPassword == "" 
                || registerUserBindingModel.Username == "")
            {
                this.ViewModel["message"] = "<p>Empty input fields !</p>";
                return View();
            }
            
            if (registerUserBindingModel.Password != registerUserBindingModel.ConfirmPassword){

                this.ViewModel["message"] = "<p>Passwords must match !</p>";
                return View();
            }
            
            var user = new User()
            {
                Username = registerUserBindingModel.Username,
                Password = PasswordUtilities.GenerateHash256(registerUserBindingModel.Password)
            };
            
            //add user to db
            using (var context = new NotesDbContext())
            {
                if (context.Users.Any(u => u.Username == registerUserBindingModel.Username))
                {
                    this.ViewModel["message"] = "<p>Username is taken !</p>";
                    return View();
                }

                context.Users.Add(user);
                context.SaveChanges();
            }

            this.SignIn(user.Username);
            
            return Redirect("/users/login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.ViewModel["message"] = "<p></p>";
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(LoginUserBindingModel loginUserBindingModel)
        {
            
            if (loginUserBindingModel.Username == "" || loginUserBindingModel.Password == "")
            {
                this.ViewModel["message"] = "Empty input fields !";
                return View();
            }

            using (var context = new NotesDbContext())
            {
                
                var foundUser = context
                    .Users
                    .FirstOrDefault(u => u.Username == loginUserBindingModel.Username 
                        && u.Password == PasswordUtilities.GenerateHash256(loginUserBindingModel.Password));

                if (foundUser == null)
                {
                    this.ViewModel["message"] = "Invalid Username or password !";
                    return View();
                }
                
                //we add it to the session storage
                context.SaveChanges();
                this.SignIn(foundUser.Username);    
            }

            return Redirect("/home/index");

        }
        
        [HttpGet]
        public IActionResult Logout()
        {

            this.SignOut();

            return Redirect("/home/index");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int id = int.Parse(this.Request.UrlParameters["id"]);

            User user = null;
            using (var context = new NotesDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.Id == id);


                if (user == null)
                {
                    this.ViewModel["username"] = "ERROR! User not found!";
                    this.ViewModel["message"] = "";
                    return View();
                }


                this.ViewModel["username"] = user.Username.ToString();
                this.ViewModel["message"] = "";
                this.ViewModel["userId"] = user.Id.ToString();

                var notes = context.Notes.Where(n => n.OwnerId == user.Id).ToArray();

                this.ViewModel["notes"] = notes.Any()
                ? string.Join(string.Empty, notes
                        .Select(u => $"<li><a href=\"/users/note?id={u.Id}\">{u.Title}</a>  -  {u.Content}</li>"))
                    : $"<p>No notes for this user !</p>";

            }
            return View();
        }

        [HttpPost]
        public IActionResult Profile(NotesBindingModel notesBindingModel)
        {

            
            User user = null;
            using (var context = new NotesDbContext())
            {

                user = context.Users.FirstOrDefault(u => u.Id == int.Parse(notesBindingModel.UserId));
                
                if (notesBindingModel.Title == "" || notesBindingModel.Content == "")
                {
                    this.ViewModel["message"] = "Empty input fields !";
                    this.ViewModel["username"] = user.Username;

                    var notes = context.Notes.Where(n => n.OwnerId == user.Id).ToArray();

                    this.ViewModel["notes"] = notes.Any()
                    ? string.Join(string.Empty, notes
                            .Select(u => $"<li><a href=\"/users/note?id={u.Id}\">{u.Title}</a>  -  {u.Content}</li>"))
                        : $"<p>No notes for this user !</p>";

                    return View();
                }


                Note note = new Note
                {
                    Content = notesBindingModel.Content,
                    Title = notesBindingModel.Title,
                    Owner = user                
                };

                context.Notes.Add(note);
                context.SaveChanges();
            }

           

            return Redirect($"/users/profile?id={user.Id}");
            
        }
        
        [HttpGet]
        public IActionResult Note()
        {

            int noteId = int.Parse(this.Request.UrlParameters["id"]);

            using (var context = new NotesDbContext())
            {
                Note note = context.Notes.Find(noteId);

                this.ViewModel["title"] = note.Title;
                this.ViewModel["content"] = note.Content;
                this.ViewModel["noteId"] = note.Id.ToString();
            }

            return View();
        }

        [HttpGet]
        public IActionResult All()
        {
            
            Dictionary<int, string> users = new Dictionary<int, string>();

            using (var context = new NotesDbContext()) {
                users = context.Users.ToDictionary(u => u.Id, u => u.Username);
            }

            this.ViewModel["users"] =
                users.Any() ? string.Join(string.Empty, users
                    .Select(u => $"<li><a href=\"/users/profile?id={u.Key}\">{u.Value}</a></li>")) 
                : $"<p>No users in databse !</p>";

            return View();
        }
        
    }
}
