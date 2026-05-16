using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var data = categoryService.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            ViewBag.Categories = categoryService.GetAll(); 
            return View(new CategoryDTO());
        }

        [HttpPost]
        public IActionResult create(CategoryDTO c)
        {
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
