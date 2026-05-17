using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class PurchaseItemDTO
    {
        public int PurchaseItemId { get; set; }

        [Required(ErrorMessage = "Purchase ID is required.")]
        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "Medicine ID is required.")]
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Cost is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Cost must be greater than 0.")]
        public decimal UnitCost { get; set; }
    }
}
