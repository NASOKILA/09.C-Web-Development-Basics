
namespace KittenApp.Web.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class RegisterUserBindingModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
