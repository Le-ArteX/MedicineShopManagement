using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class SaleController : Controller
    {
        private readonly SaleService _saleService;

        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }

        public IActionResult Index()
        {
            var data = _saleService.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SaleDTO());
        }

        [HttpPost]
        public IActionResult Create(SaleDTO p)
        {
            if (ModelState.IsValid)
            {
                var res = _saleService.Create(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Sale added successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
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
        public IActionResult Details(int id)
        {
            var sale = _saleService.Get(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
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
