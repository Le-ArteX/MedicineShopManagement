using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MedicineShop.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly AuthService _authService;

        public ForgotPasswordController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reset(string email, string newPassword, string confirmPassword, string interestedOn)
        {
            ViewBag.Email = email;
            ViewBag.InterestedOn = interestedOn;

            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Please enter a valid email address.";
                return View("Index");
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("Index");
            }

            email = email.Trim();
            interestedOn = interestedOn?.Trim() ?? "";

            var user = _authService.GetUserByEmail(email);
            if (user == null)
            {
                ViewBag.Error = "No account found with this email.";
                return View("Index");
            }

            if (string.IsNullOrWhiteSpace(user.InterestedOn) || !user.InterestedOn.Trim().Equals(interestedOn, StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "Interested field does not match our records.";
                return View("Index");
            }

            bool result = _authService.ResetPassword(email, newPassword);
            if (result)
            {
                TempData["SuccessMessage"] = "Password has been reset successfully. You can now login.";
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Error = "Something went wrong. Please try again.";
            return View("Index");
        }

            }
        }
