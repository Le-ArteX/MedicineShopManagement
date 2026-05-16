using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
