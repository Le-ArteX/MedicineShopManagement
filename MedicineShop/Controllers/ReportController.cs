using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
