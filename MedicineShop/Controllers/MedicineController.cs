using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class MedicineController : Controller
    {
        private readonly MedicineService _medicineService;

        public MedicineController(MedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public IActionResult Index()
        {
            var data = _medicineService.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View(new MedicineDTO());
        }

        [HttpPost]
        public IActionResult create(MedicineDTO m)
        {
            if (ModelState.IsValid)
            {
                var res = _medicineService.Create(m);
                if (res)
                {
                    TempData["SuccessMessage"] = "Medicine added successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(m);
        }
        [HttpGet]
        public IActionResult update(int id)
        {
            var medicine = _medicineService.Get(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        [HttpPost]
        public IActionResult update(MedicineDTO m)
        {
            if (ModelState.IsValid)
            {
                var res = _medicineService.Update(m);
                if (res)
                {
                    TempData["SuccessMessage"] = "Medicine updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(m);
        }

        [HttpGet]
        public IActionResult details(int id)
        {
            var medicine = _medicineService.Get(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        [HttpGet]
        public IActionResult delete(int id)
        {
            var medicine = _medicineService.Get(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult confirmDelete(int MedicineId)
        {
            var res = _medicineService.Delete(MedicineId);
            if (res)
            {
                TempData["SuccessMessage"] = "Medicine deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
