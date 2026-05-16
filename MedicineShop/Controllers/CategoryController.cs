using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
