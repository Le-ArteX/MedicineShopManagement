using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class LoginController : AppController
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Urole");
            if (role == "Admin") return RedirectToAction("Index", "Admin");
            if (role == "Staff") return RedirectToAction("Index", "Staff");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var role = HttpContext.Session.GetString("Urole");
            if (role == "Admin") return RedirectToAction("Index", "Admin");
            if (role == "Staff") return RedirectToAction("Index", "Staff");

            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var user = _authService.Authenticate(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginDto);
            }

            HttpContext.Session.SetString("Uname", user.Name);
            HttpContext.Session.SetString("Uemail", user.Email);
            HttpContext.Session.SetString("Urole", user.Role);

            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            if (user.Role == "Staff")
            {
                return RedirectToAction("Index", "Staff");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}