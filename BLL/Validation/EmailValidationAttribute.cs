
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BLL.Validations
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            string email = value.ToString();


            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, pattern))
            {
                return new ValidationResult(ErrorMessage ?? "Please enter a valid email address");
            }

            return ValidationResult.Success;
        }
    }
}