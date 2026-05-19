using System;
using System.Linq;
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

        public IActionResult Index(int page = 1)
        {
            if (!(IsAdmin || IsStaff))
            {
                return LoginRedirect();
            }

            const int pageSize = 15;
            var lowStockMedicines = _reportService.GetLowStockMedicines();
            var totalCount = lowStockMedicines.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages == 0 ? 1 : totalPages));
            var pagedLowStockMedicines = lowStockMedicines.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var reportDto = new BLL.DTOs.ReportDTO
            {
                DashboardSummary = _reportService.GetDashboardSummary(),
                LowStockMedicines = pagedLowStockMedicines
            };

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(reportDto);
        }
    }
}
