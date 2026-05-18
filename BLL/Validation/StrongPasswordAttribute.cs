
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BLL.Validations
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            string password = value.ToString();

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";

            if (!Regex.IsMatch(password, pattern))
            {
                return new ValidationResult(ErrorMessage ?? "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            }

            return ValidationResult.Success;
        }
    }
}