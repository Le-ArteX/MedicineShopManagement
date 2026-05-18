using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class ReportController : AppController
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var reportDto = new ReportDTO
            {
                DashboardSummary = _reportService.GetDashboardSummary(),
                LowStockMedicines = _reportService.GetLowStockMedicines()
            };

            return View(reportDto);
        }
    }
}
