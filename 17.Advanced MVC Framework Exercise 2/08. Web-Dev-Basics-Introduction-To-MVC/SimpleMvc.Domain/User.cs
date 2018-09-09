
namespace SimpleMvc.Domain
{
    using System.Collections.Generic;

    public class User
    {

        public User()
        {
            this.Notes = new List<Note>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
