using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Contact Person name is required.")]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = null!;
    }
}
