using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        
        public IActionResult Index()
        {
            var data = _supplierService.GetAll();
            return View(data);
        }

       
        [HttpGet]
        public IActionResult create()
        {
            return View(new SupplierDTO());
        }

        [HttpPost]
        public IActionResult create(SupplierDTO s)
        {
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
