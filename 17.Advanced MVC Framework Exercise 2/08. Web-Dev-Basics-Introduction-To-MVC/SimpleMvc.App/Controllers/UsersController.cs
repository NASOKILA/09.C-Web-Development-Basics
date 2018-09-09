namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using SimpleMvc.App.Utilities;
    using SimpleMvc.Data;
    using SimpleMvc.Domain;
    using SimpleMvs.Framework.Attributes.Methods;
    using SimpleMvs.Framework.Controllers;
    using SimpleMvs.Framework.Interfaces;
    using SimpleMvs.Framework.Security;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            this.Model["message"] = "<p></p>";
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {

            if (!this.IsValidModel(registerUserBindingModel)) {

                this.Model["message"] = "<p>Something went wrong !</p>";
                return View();
            }

            if (registerUserBindingModel.Password == "" 
                || registerUserBindingModel.ConfirmPassword == "" 
                || registerUserBindingModel.Username == "")
            {

                this.Model["message"] = "<p>Empty input fields !</p>";
                return View();
            }




            if (registerUserBindingModel.Password != registerUserBindingModel.ConfirmPassword){

                this.Model["message"] = "<p>Passwords must match !</p>";
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
                    this.Model["message"] = "<p>Username is taken !</p>";
                    return View();
                }

                context.Users.Add(user);
                context.SaveChanges();
            }
            
            return RedirectToAction("/users/login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.Model["message"] = "<p></p>";
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(LoginUserBindingModel loginUserBindingModel)
        {



            if (loginUserBindingModel.Username == "" || loginUserBindingModel.Password == "")
            {
                this.Model["message"] = "Empty input fields !";
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
                    this.Model["message"] = "Invalid Username or password !";
                    return View();
                }
                
                //we add it to the session storage
                context.SaveChanges();
                this.SignIn(foundUser.Username);
            }

            return RedirectToAction("/home/index");

        }


        [HttpGet]
        public IActionResult Logout()
        {

            this.SignOut();

            return RedirectToAction("/home/index");
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
                    this.Model["username"] = "ERROR! User not found!";
                    this.Model["message"] = "";
                    return View();
                }


                this.Model["username"] = user.Username.ToString();
                this.Model["message"] = "";
                this.Model["userId"] = user.Id.ToString();

                var notes = context.Notes.Where(n => n.OwnerId == user.Id).ToArray();

                this.Model["notes"] = notes.Any()
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
                    this.Model["message"] = "Empty input fields !";
                    this.Model["username"] = user.Username;

                    var notes = context.Notes.Where(n => n.OwnerId == user.Id).ToArray();

                    this.Model["notes"] = notes.Any()
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

           

            return RedirectToAction($"/users/profile?id={user.Id}");
            
        }

        
        [HttpGet]
        public IActionResult Note()
        {

            int noteId = int.Parse(this.Request.UrlParameters["id"]);

            using (var context = new NotesDbContext())
            {
                Note note = context.Notes.Find(noteId);

                this.Model["title"] = note.Title;
                this.Model["content"] = note.Content;
                this.Model["noteId"] = note.Id.ToString();
            }

            return View();
        }



        [HttpGet]
        public IActionResult All()
        {

            //the authentiction is not working correctly
            /*if (!this.User.IsAuthenticated)
            {
                return RedirectToAction("/users/login");
            }*/

            Dictionary<int, string> users = new Dictionary<int, string>();

            using (var context = new NotesDbContext()) {
                users = context.Users.ToDictionary(u => u.Id, u => u.Username);
            }

            this.Model["users"] =
                users.Any() ? string.Join(string.Empty, users
                    .Select(u => $"<li><a href=\"/users/profile?id={u.Key}\">{u.Value}</a></li>")) 
                : $"<p>No users in databse !</p>";

            return View();
        }


        /*
        [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            List<string> allUsersUsernames = null;

            //add user to db
            using (var context = new NotesDbContext())
            {
                allUsersUsernames = context
                    .Users
                    .Select(u => u.Username)
                    .ToList();    
            }

            var viewModel = new AllUsernamesViewModel()
            {
                Usernames = allUsersUsernames
            };

            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            User user = null;
            List<Note> notes = null;

            //add user to db
            using (var context = new NotesDbContext())
            {
                user = context
                    .Users
                    .SingleOrDefault(u => u.Id == id);

                notes = context
                    .Notes
                    .Where(n => n.OwnerId == user.Id).ToList();
            }

            
            var viewModel = new UserProfileViewModel()
            {
                Username = user.Username,
                UserId = user.Id,
                Notes = notes.Select(n => 
                    new NoteViewModel()
                    {
                        Title = n.Title,
                        Content = n.Content
                    }
                )
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            
            using (var context = new NotesDbContext())
            {
                //vzimam usera ot modela
                User user = context
                    .Users
                    .SingleOrDefault(u => u.Id == model.UserId);

                //vzimam beejkata ot modela
                Note note = new Note
                {
                    Title = model.Title,
                    Content = model.Content,
                    OwnerId = model.UserId,
                    Owner = user
                };

                //zapazvam beljkata v bazata
                context.Notes.Add(note);
                context.SaveChanges();

            };

            //vrushtam se pak tuk kato podavam idto na usera
            return Profile(model.UserId);
        }
        */
    }
}
