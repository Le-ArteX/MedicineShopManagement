using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public abstract class AppController : Controller
    {
        protected bool IsLoggedIn => !string.IsNullOrEmpty(HttpContext.Session.GetString("Uemail"));
        protected bool IsAdmin => HttpContext.Session.GetString("Urole") == "Admin";
        protected bool IsStaff => HttpContext.Session.GetString("Urole") == "Staff";

        protected IActionResult LoginRedirect() => RedirectToAction("Login", "Login");
        protected IActionResult HomeRedirect() => RedirectToAction("Index", "Home");
    }
}
