namespace KittenApp.Web.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class AddKittenBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)] //we limit the age from 0 to 20 
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }
        
    }
}
