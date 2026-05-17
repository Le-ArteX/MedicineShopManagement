using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            var reportDto = new ReportDTO
            {
                DashboardSummary = _reportService.GetDashboardSummary(),
                LowStockMedicines = _reportService.GetLowStockMedicines()
            };

            return View(reportDto);
        }
    }
}
