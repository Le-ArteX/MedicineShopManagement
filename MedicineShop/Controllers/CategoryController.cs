using System.Linq;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CategoryController : AppController
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index(string q, int page = 1)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var data = categoryService.GetAll();
            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(item =>
                    (item.Name?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Describe?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    item.CategoryId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase)).ToList();
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

            ViewBag.Categories = categoryService.GetAll(); 
            return View(new CategoryDTO());
        }

        [HttpPost]
        public IActionResult create(CategoryDTO c)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if(ModelState.IsValid)
            {
                var res = categoryService.Create(c);
                if (res)
                {
                    TempData["SuccessMessage"] = "Category created successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(c);
        }

        [HttpGet]
        public IActionResult update(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var category = categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult update(CategoryDTO c)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid) { 

                var res = categoryService.Update(c);
                if (res)
                {
                    TempData["SuccessMessage"] = "Category updated successfully!";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Categories = categoryService.GetAll();

            return View(c);
        }

        [HttpGet]
        public IActionResult details(int id) 
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var category = categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category); 
        }


        [HttpGet]
        public IActionResult delete(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var category = categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category); 
        }

        [HttpPost]
        [ActionName("delete")] 
        public IActionResult confirmDelete(int CategoryId)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var res = categoryService.Delete(CategoryId);
            if (res)
            {
                TempData["SuccessMessage"] = "Category deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
