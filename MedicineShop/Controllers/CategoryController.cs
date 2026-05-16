using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CategoryController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult create()
        {
            return View();
        }

        public IActionResult update()
        {
            return View();
        }

        public IActionResult delete()
        {
            return View();
        }

        public IActionResult details() {
            return View();
        }


    }
}
