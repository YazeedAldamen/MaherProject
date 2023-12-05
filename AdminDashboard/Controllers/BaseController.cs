using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> PackageTypes = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "0",
                Text = "Select Package Type"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Mix City"
            },
            new SelectListItem
            {
                Value = "2",
                Text = "Adventures"
            },
            new SelectListItem
            {
                Value = "3",
                Text = "Nature Lovers"
            },
            new SelectListItem
            {
                Value = "4",
                Text = "Honeymoon"
            }
        };

        public List<SelectListItem> selectListItems = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "0",
                Text = "Select Room Class"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Economic"
            },
            new SelectListItem
            {
                Value = "2",
                Text = "Luxury"
            }
        };
    }
}
