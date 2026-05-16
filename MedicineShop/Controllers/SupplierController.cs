using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
