using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class AlertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
