using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class SettingsController : AppController
    {
        private readonly AuthService _authService;

        public SettingsController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            if (!IsAdmin && !IsStaff)
            {
                return HomeRedirect();
            }

            var currentEmail = HttpContext.Session.GetString("Uemail");
            var user = _authService.GetUserByEmail(currentEmail);
            if (user == null)
            {
                return HomeRedirect();
            }

            var model = new SettingsDTO
            {
                Name = user.Name,
                Email = user.Email,
                InterestedOn = user.InterestedOn
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SettingsDTO model)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            if (!IsAdmin && !IsStaff)
            {
                return HomeRedirect();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(model.NewPassword) && string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                ModelState.AddModelError(nameof(model.ConfirmPassword), "Confirm password is required.");
                return View(model);
            }

            var currentEmail = HttpContext.Session.GetString("Uemail");
            var result = _authService.UpdateProfile(currentEmail, model);
            if (result != "Success")
            {
                ModelState.AddModelError(string.Empty, result);
                return View(model);
            }

            HttpContext.Session.SetString("Uemail", model.Email);
            HttpContext.Session.SetString("Uname", model.Name);
            ViewBag.Success = "Settings updated successfully.";
            ModelState.Clear();

            return View(model);
        }
    }
}
