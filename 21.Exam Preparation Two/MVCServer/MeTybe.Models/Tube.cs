namespace MeTybe.Models
{

    using System.ComponentModel.DataAnnotations;

    public class Tube
    {

        public Tube()
        {
            this.Views = 0;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }
                
        [Required]
        public string YoutubeId { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public User Uploader { get; set; }

        public int UploaderId { get; set; }
    }
}