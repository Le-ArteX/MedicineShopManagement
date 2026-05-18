using System.Linq;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class SupplierController : AppController
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index(string q, int page = 1)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var data = _supplierService.GetAll();
            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(item =>
                    (item.Name?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.ContactPerson?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Phone?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Email?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    item.SupplierId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            const int pageSize = 15;
            var totalCount = data.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages == 0 ? 1 : totalPages));
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["SearchQuery"] = q;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            return View(pagedData);
        }

        [HttpGet]
        public IActionResult create()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            return View(new SupplierDTO());
        }

        [HttpPost]
        public IActionResult create(SupplierDTO s)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _supplierService.Create(s);
                if (res)
                {
                    TempData["SuccessMessage"] = "Supplier added successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(s);
        }

        [HttpGet]
        public IActionResult update(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var supplier = _supplierService.Get(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        public IActionResult update(SupplierDTO s)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _supplierService.Update(s);
                if (res)
                {
                    TempData["SuccessMessage"] = "Supplier updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(s);
        }

        [HttpGet]
        public IActionResult details(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var supplier = _supplierService.Get(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpGet]
        public IActionResult delete(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var supplier = _supplierService.Get(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult confirmDelete(int SupplierId)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var res = _supplierService.Delete(SupplierId);
            if (res)
            {
                TempData["SuccessMessage"] = "Supplier deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
