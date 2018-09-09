namespace MeTybe.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {

        public User()
        {
            this.Tubes = new List<Tube>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(10)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Tube> Tubes { get; set; }
    }
}
