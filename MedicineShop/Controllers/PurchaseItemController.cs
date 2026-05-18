using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class PurchaseItemController : AppController
    {
        private readonly PurchaseItemService _purchaseItemService;

        public PurchaseItemController(PurchaseItemService purchaseItemService)
        {
            _purchaseItemService = purchaseItemService;
        }

        public IActionResult Index()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var data = _purchaseItemService.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            return View(new PurchaseItemDTO());
        }

        [HttpPost]
        public IActionResult create(PurchaseItemDTO p)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _purchaseItemService.Create(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Purchase Item added successfully!";
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

            var purchaseItem = _purchaseItemService.Get(id);
            if (purchaseItem == null)
            {
                return NotFound();
            }
            return View(purchaseItem);
        }

        [HttpPost]
        public IActionResult update(PurchaseItemDTO p)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = _purchaseItemService.Update(p);
                if (res)
                {
                    TempData["SuccessMessage"] = "Purchase Item updated successfully!";
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

            var purchaseItem = _purchaseItemService.Get(id);
            if (purchaseItem == null)
            {
                return NotFound();
            }
            return View(purchaseItem);
        }

        [HttpGet]
        public IActionResult delete(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var purchaseItem = _purchaseItemService.Get(id);
            if (purchaseItem == null)
            {
                return NotFound();
            }
            return View(purchaseItem);
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult confirmDelete(int PurchaseItemId)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var res = _purchaseItemService.Delete(PurchaseItemId);
            if (res)
            {
                TempData["SuccessMessage"] = "Purchase Item deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
