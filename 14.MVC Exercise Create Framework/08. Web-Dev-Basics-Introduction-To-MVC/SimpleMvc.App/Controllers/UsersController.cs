namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using SimpleMvc.App.Utilities;
    using SimpleMvc.App.ViewModels;
    using SimpleMvc.Data;
    using SimpleMvc.Domain;
    using SimpleMvs.Framework.Attributes.Methods;
    using SimpleMvs.Framework.Controllers;
    using SimpleMvs.Framework.Interfaces;
    using SimpleMvs.Framework.Interfaces.Generic;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel registerUserBindingModel)
        {
            var user = new User()
            {
                Username = registerUserBindingModel.Username,
                Password = PasswordUtilities.GenerateHash256(registerUserBindingModel.Password)
            };
            
            //add user to db
            using (var context = new NotesDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View();
        }

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
        
    }
}
