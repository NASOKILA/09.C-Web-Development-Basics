using System.ComponentModel.DataAnnotations;

namespace SimpleMvc.Domain
{
    public class Note
    {
        public Note()
        {}

        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        public string Content  { get; set; }

        public User Owner { get; set; }

        [Required]
        public int OwnerId { get; set; }
    }
}
