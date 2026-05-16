using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class MedicineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
