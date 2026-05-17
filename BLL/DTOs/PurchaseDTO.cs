using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "Purchase Date is required")]
        public DateOnly PurchaseDate { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than zero")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Invoice Number is required")]
        [StringLength(100, ErrorMessage = "Invoice Number cannot exceed 100 characters")]
        public string InvoiceNo { get; set; } = null!;

        [Required(ErrorMessage = "Supplier ID is required")]
        public int SupplierId { get; set; }
    }
}
