using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace MedicineShop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AuthService _authService;

        public RegisterController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public IActionResult Register(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
              
                var resultMessage = _authService.Register(userDto   );

                if (resultMessage == "Success")
                {
                    TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                    return RedirectToAction("Login", "Login");
                }

                ModelState.AddModelError("", resultMessage);
            }

            return View(userDto);
        }
    }
}