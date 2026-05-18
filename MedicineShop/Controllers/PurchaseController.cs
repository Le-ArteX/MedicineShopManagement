using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class PurchaseController : AppController
    {
        private readonly PurchaseService _purchaseService;

        public PurchaseController(PurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public IActionResult Index(string q)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var data = _purchaseService.Get();
            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(item =>
                    (item.InvoiceNo?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    item.SupplierId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.PurchaseId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.PurchaseDate.ToString("dd-MMM-yyyy").Contains(q, System.StringComparison.OrdinalIgnoreCase) ||
                    item.TotalAmount.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewData["SearchQuery"] = q;
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            return View(new PurchaseDTO());
        }

        [HttpPost]
        public IActionResult create(PurchaseDTO p)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _purchaseService.Create(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Purchase added successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult update(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var purchase = _purchaseService.Get(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        [HttpPost]
        public IActionResult update(PurchaseDTO p)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _purchaseService.Update(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Purchase updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult details(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var purchase = _purchaseService.Get(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        [HttpGet]
        public IActionResult delete(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var purchase = _purchaseService.Get(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult confirmDelete(int PurchaseId)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var res = _purchaseService.Delete(PurchaseId);
            if (res)
            {
                TempData["SuccessMessage"] = "Purchase deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
