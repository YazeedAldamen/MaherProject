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

        public List<SelectListItem> RoomClass = new List<SelectListItem>
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

        public List<SelectListItem> Cities = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "0",
                Text = "Select City"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Baku"
            },
            new SelectListItem
            {
                Value = "2",
                Text = "Ganja"
            },
            new SelectListItem
            {
                Value = "3",
                Text = "Shaki"
            },
            new SelectListItem
            {
                Value = "4",
                Text = "Lankaran"
            },
            new SelectListItem
            {
                Value = "5",
                Text = "Quba"
            },
            new SelectListItem
            {
                Value = "6",
                Text = "Qabala"
            },
            new SelectListItem
            {
                Value = "7",
                Text = "Sheki"
            },
            new SelectListItem
            {
                Value = "8",
                Text = "Guba"
            },
            new SelectListItem
            {
                Value = "9",
                Text = "Khachmaz"
            },
            new SelectListItem
            {
                Value = "10",
                Text = "Gabala"
            },
            new SelectListItem
            {
                Value = "11",
                Text = "Mingachevir"
            },
            new SelectListItem
            {
                Value = "12",
                Text = "Sumqayit"
            },
            new SelectListItem
            {
                Value = "13",
                Text = "Nakhchivan"
            },
            new SelectListItem
            {
                Value = "14",
                Text = "Shamakhi"
            },
            new SelectListItem
            {
                Value = "15",
                Text = "Goychay"
            },
            new SelectListItem
            {
                Value = "16",
                Text = "Lerik"
            },
            new SelectListItem
            {
                Value = "17",
                Text = "Masalli"
            },
            new SelectListItem
            {
                Value = "18",
                Text = "Astara"
            },
            new SelectListItem
            {
                Value = "19",
                Text = "Ordubad"
            },
            new SelectListItem
            {
                Value = "20",
                Text = "Goygol"
            },
            new SelectListItem
            {
                Value = "21",
                Text = "Balakan"
            },
            new SelectListItem
            {
                Value = "22",
            }
        };
    }
}
