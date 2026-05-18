using System.ComponentModel.DataAnnotations;
using BLL.Validations;

namespace BLL.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]

        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailValidationAttribute]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StrongPasswordAttribute]
        public string Password { get; set; } = null!;

        [Required]
        [StrongPasswordAttribute]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = null!; 

        [Required(ErrorMessage = "Interested On is required.")]
        public string InterestedOn { get; set; } = null!;
    }
}