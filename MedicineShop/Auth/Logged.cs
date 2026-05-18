using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MedicineShop.Auth.AuthFilters
{
    public class Logged : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var data = context.HttpContext.Session.GetString("Uname");
            if (data == null)
            {
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }
        }
    }
}