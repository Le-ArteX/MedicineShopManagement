using System.Linq;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class MedicineController : AppController
    {
        private readonly MedicineService _medicineService;

        public MedicineController(MedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public IActionResult Index(string q, int page = 1)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var data = _medicineService.GetAll();
            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(item =>
                    (item.Name?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.GenericName?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Brand?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    item.MedicineId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase)).ToList();
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

            return View(new MedicineDTO());
        }

        [HttpPost]
        public IActionResult create(MedicineDTO m)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

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
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

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
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

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
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

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
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

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
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

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
