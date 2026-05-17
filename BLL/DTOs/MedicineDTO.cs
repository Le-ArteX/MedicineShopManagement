using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class MedicineDTO
    {
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Medicine Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Generic Name is required.")]
        public string GenericName { get; set; } = null!;

        [Required(ErrorMessage = "Brand is required.")]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than zero.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        [Required]
        public int MinStockLevel { get; set; }

        [Required]
        public DateOnly ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please select a Category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please select a Supplier.")]
        public int SupplierId { get; set; }
    }
}
