using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class StaffController : AppController
    {
        public IActionResult Index()
        {
            if (!IsStaff)
            {
                return LoginRedirect();
            }

            return View();
        }
    }
}
