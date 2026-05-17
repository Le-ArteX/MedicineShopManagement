using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class ReportDTO
    {
        [Required]
        public DashboardSummaryDTO DashboardSummary { get; set; } = new DashboardSummaryDTO();

        [Required]
        public List<LowStockMedicineDTO> LowStockMedicines { get; set; } = new List<LowStockMedicineDTO>();
    }

    public class DashboardSummaryDTO
    {
        [Required(ErrorMessage = "Total Sales Revenue is required")]
        public decimal TotalSalesRevenue { get; set; }

        [Required(ErrorMessage = "Total Sales Count is required")]
        public int TotalSalesCount { get; set; }

        [Required(ErrorMessage = "Total Purchase Cost is required")]
        public decimal TotalPurchaseCost { get; set; }

        [Required(ErrorMessage = "Total Purchase Count is required")]
        public int TotalPurchaseCount { get; set; }

        [Required(ErrorMessage = "Total Medicines count is required")]
        public int TotalMedicines { get; set; }

        [Required(ErrorMessage = "Low Stock Medicine Count is required")]
        public int LowStockMedicineCount { get; set; }
    }

    public class LowStockMedicineDTO
    {
        [Required(ErrorMessage = "Medicine ID is required")]
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Medicine Name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "Stock Quantity is required")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Minimum Stock Level is required")]
        public int MinStockLevel { get; set; }
    }
}
