using System.ComponentModel.DataAnnotations;
using BLL.Validations;

namespace BLL.DTOs
{
    public class SettingsDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailValidationAttribute]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Interested field is required.")]
        public string InterestedOn { get; set; } = null!;

        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
