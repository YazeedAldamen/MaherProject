using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminDashboard.Controllers
{
    public class BaseController : Controller
    {
        protected void ShowSuccessMessage(string message)
        {
            ShowAlertPopup(message, "Success", "success");
        }

        protected void ShowErrorMessage(string message)
        {
            ShowAlertPopup(message, "Error", "error");
        }

        [NonAction]
        protected void ShowAlertPopup(string message, string title, string icon, string? returnUrl = null)
        {
            var data = new
            {
                swal_message = message,
                title = title,
                icon = icon,
                returnUrl = returnUrl
            };

            string jsonData = JsonConvert.SerializeObject(data);

            // Create a new notification cookie
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30) // Set cookie expiration time
            };
            HttpContext.Response.Cookies.Append("notification", jsonData, cookieOptions);
        }
    }
}
