using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;

        public CustomerController(CustomerService customerService)
        {
            this.customerService = customerService;
        }
        public IActionResult Index()
        {
            var data = customerService  .GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            ViewBag.Customers = customerService.GetAll();
            return View(new CustomerDTO());
        }

        [HttpPost]
        public IActionResult create(CustomerDTO c)
        {
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
            if (ModelState.IsValid)
            {

                var res = customerService.Update(c);
                if (res)
                {
                    TempData["SuccessMessage"] = "Customer       updated successfully!";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Customers = customerService.GetAll();

            return View(c);
        }

        [HttpGet]
        public IActionResult details(int id)
        {
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
