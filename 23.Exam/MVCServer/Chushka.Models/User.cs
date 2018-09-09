using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chushka.Models
{
    public class User
    {

        public User()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
        
        public Role Role { get; set; }

        public int RoleId { get; set; }

        public ICollection<Order> Orders { get; set; } 
    }
}
