using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class SaleController : AppController
    {
        private readonly SaleService _saleService;

        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }

        public IActionResult Index(string q)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var data = _saleService.Get();
            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(item =>
                    (item.InvoiceNo?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    item.CustomerId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.SaleId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.SaleDate.ToString("dd-MMM-yyyy").Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.TotalAmount.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.Discount.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewData["SearchQuery"] = q;
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            return View(new SaleDTO
            {
                SaleDate = DateOnly.FromDateTime(DateTime.Today)
            });
        }

        [HttpPost]
        public IActionResult Create(SaleDTO p)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _saleService.Create(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Sale added successfully! Invoice generated.";
                    return RedirectToAction("Invoice", new { invoiceNo = p.InvoiceNo });
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult Invoice(string invoiceNo)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var sale = _saleService.GetByInvoiceNo(invoiceNo);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var sale = _saleService.Get(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View("Invoice", sale);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var sale = _saleService.Get(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        [HttpPost]
        public IActionResult Update(SaleDTO p)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _saleService.Update(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Sale updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var sale = _saleService.Get(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int SaleId)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var res = _saleService.Delete(SaleId);
            if (res)
            {
                TempData["SuccessMessage"] = "Sale deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
