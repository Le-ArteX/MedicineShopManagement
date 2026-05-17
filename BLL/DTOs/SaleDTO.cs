using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class SaleDTO
    {
        public int SaleId { get; set; }

        [Required(ErrorMessage = "Sale Date is required")]
        public DateOnly SaleDate { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than zero")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Discount cannot be negative")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Invoice Number is required")]
        [StringLength(100, ErrorMessage = "Invoice Number cannot exceed 100 characters")]
        public string InvoiceNo { get; set; } = null!;

        [Required(ErrorMessage = "Customer ID is required")]
        public int CustomerId { get; set; }
    }
}
