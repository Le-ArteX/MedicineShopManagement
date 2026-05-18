using System.Collections.Generic;

namespace BLL.DTOs
{
    public class AdminDTO
    {
        public int TotalMedicines { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int ActiveAlertsCount { get; set; }

        public int TotalSales { get; set; }
        public int TotalPurchases { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalStaff { get; set; }

        public List<SaleDTO> RecentSales { get; set; } = new List<SaleDTO>();
        public List<UserDTO> StaffUsers { get; set; } = new List<UserDTO>();
    }
}
