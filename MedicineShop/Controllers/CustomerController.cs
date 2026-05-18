using System.Linq;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CustomerController : AppController
    {
        private readonly CustomerService customerService;

        public CustomerController(CustomerService customerService)
        {
            this.customerService = customerService;
        }
        public IActionResult Index(string q)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var data = customerService.GetAll();
            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(item =>
                    (item.Name?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Phone?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Email?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Address?.Contains(q, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    item.CustomerId.ToString().Contains(q, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewData["SearchQuery"] = q;
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            ViewBag.Customers = customerService.GetAll();
            return View(new CustomerDTO());
        }

        [HttpPost]
        public IActionResult create(CustomerDTO c)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {
                var res = customerService.Create(c);
                if (res)
                {
                    TempData["SuccessMessage"] = "Customer created successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(c);
        }

        [HttpGet]
        public IActionResult update(int id)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var customer = customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult update(CustomerDTO c)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            if (ModelState.IsValid)
            {

                var res = customerService.Update(c);
                if (res)
                {
                    TempData["SuccessMessage"] = "Customer updated successfully!";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Customers = customerService.GetAll();

            return View(c);
        }

        [HttpGet]
        public IActionResult details(int id)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var customer = customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }


        [HttpGet]
        public IActionResult delete(int id)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var customer = customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult confirmDelete(int CustomerId)
        {
            if (!IsLoggedIn)
            {
                return LoginRedirect();
            }

            var res = customerService.Delete(CustomerId);
            if (res)
            {
                TempData["SuccessMessage"] = "Customer deleted permanently!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}
