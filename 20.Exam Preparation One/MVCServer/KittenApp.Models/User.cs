using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KittenApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
