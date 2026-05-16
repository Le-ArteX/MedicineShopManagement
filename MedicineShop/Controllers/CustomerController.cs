using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
