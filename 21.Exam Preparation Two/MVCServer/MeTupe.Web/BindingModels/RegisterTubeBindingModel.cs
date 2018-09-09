namespace MeTupe.Web.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class RegisterTubeBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Author { get; set; }
        
        [Required]
        [StringLength(11)]
        public string YoutubeId { get; set; }
        
        public string Description { get; set; }
    }
}
