using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
