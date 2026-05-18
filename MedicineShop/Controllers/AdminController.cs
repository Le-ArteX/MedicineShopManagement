using System.Linq;
using BLL.DTOs;
using BLL.Services;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class AdminController : AppController
    {
        private readonly AdminService _adminService;
        private readonly AuthService _authService;
        private readonly UserRepo _userRepo;

        public AdminController(AdminService adminService, AuthService authService, UserRepo userRepo)
        {
            _adminService = adminService;
            _authService = authService;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var dashboardDto = _adminService.GetDashboardData();
            return View(dashboardDto);
        }

        public IActionResult Staff(int page = 1)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var staffUsers = _userRepo.GetByRole("Staff");
            var staffDtos = staffUsers.Select(u => new UserDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            const int pageSize = 15;
            var totalCount = staffDtos.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages == 0 ? 1 : totalPages));
            var pagedData = staffDtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(pagedData);
        }

        [HttpGet]
        public IActionResult CreateStaff()
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            return View(new UserDTO { Role = "Staff" });
        }

        [HttpPost]
        public IActionResult CreateStaff(UserDTO userDto)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            userDto.Role = "Staff";

            if (ModelState.IsValid)
            {
                var resultMessage = _authService.Register(userDto);
                if (resultMessage == "Success")
                {
                    TempData["SuccessMessage"] = "Staff account created successfully.";
                    return RedirectToAction(nameof(Staff));
                }

                ModelState.AddModelError(string.Empty, resultMessage);
            }

            return View(userDto);
        }

        [HttpGet]
        public IActionResult DeleteStaff(int id)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var user = _userRepo.Get(id);
            if (user == null || user.Role != "Staff")
            {
                return NotFound();
            }

            return View(new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpPost]
        [ActionName("DeleteStaff")]
        public IActionResult ConfirmDeleteStaff(int UserId)
        {
            if (!IsAdmin)
            {
                return LoginRedirect();
            }

            var res = _userRepo.Delete(UserId);
            if (res)
            {
                TempData["SuccessMessage"] = "Staff account deleted successfully.";
            }

            return RedirectToAction(nameof(Staff));
        }
    }
}
