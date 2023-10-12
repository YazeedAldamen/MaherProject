using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ImageService
    {
        public static async Task<string> UploadFile(IFormFile file)
        {
            string fullName = string.Empty;

            var stream = file.OpenReadStream();

            if (stream is not null)
            {
                string imageFileName = Guid.NewGuid().ToString().ToLower();
                string imageExtension = Path.GetExtension(file.FileName.ToLower());

                await FileManager.UploadFile("wwwroot/images/content/", imageFileName + imageExtension, null, stream);

                fullName = $"{imageFileName}{imageExtension}";
            }

            return fullName;
        }

    }

    

}
