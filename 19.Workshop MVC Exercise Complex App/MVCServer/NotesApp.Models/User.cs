using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class User
    {
        public User()
        {
            this.Notes = new List<Note>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
