using System;
using System.Linq;
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

        public IActionResult Index(int page = 1)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            const int pageSize = 15;
            var lowStockMedicines = _reportService.GetLowStockMedicines();
            var totalCount = lowStockMedicines.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages == 0 ? 1 : totalPages));
            var pagedLowStockMedicines = lowStockMedicines.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var reportDto = new ReportDTO
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
