using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class AlertController : AppController
    {
        private readonly ReportService _reportService;

        public AlertController(ReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            if (!(IsAdmin || IsStaff))
            {
                return LoginRedirect();
            }

            var reportDto = new BLL.DTOs.ReportDTO
            {
                DashboardSummary = _reportService.GetDashboardSummary(),
                LowStockMedicines = _reportService.GetLowStockMedicines()
            };

            return View(reportDto);
        }
    }
}
