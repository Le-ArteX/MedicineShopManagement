using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
