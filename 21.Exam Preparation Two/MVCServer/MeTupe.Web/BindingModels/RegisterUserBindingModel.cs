using System.ComponentModel.DataAnnotations;

namespace MeTupe.Web.BindingModels
{    
    public class RegisterUserBindingModel
    {
        [Required(ErrorMessage = "Username is required !")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5 characters !")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email Password is required !")]
        [DataType(DataType.EmailAddress)]
        [MinLength(5, ErrorMessage = "Minimum Length is 10 characters !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required !")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
