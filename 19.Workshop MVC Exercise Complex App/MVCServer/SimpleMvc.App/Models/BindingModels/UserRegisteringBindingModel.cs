namespace NotesApp.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisteringBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
