using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<string> HandleImage(string oldImage, IFormFile newImage)
        {
            string? newImageName = null;
            if (newImage != null)
            {
                if (!string.IsNullOrEmpty(oldImage))
                {
                    FileManager.DeleteFile(oldImage);
                }
                newImageName = await ImageService.UploadFile(newImage);
                return newImageName;
            }
            return oldImage;
        }
        public async Task<string> HandleMultipleImages(string oldImage, IFormFile newImage)
        {
            string? newImageName = null;
            if (newImage != null)
            {
                if (!string.IsNullOrEmpty(oldImage))
                {
                    FileManager.DeleteFile(oldImage);
                }
                newImageName = await ImageService.UploadFile(newImage);
            }
            return newImageName;
        }

        public async Task<string> HandleMultipleImages(List<IFormFile> imageFiles, bool isEdit = false, string oldImages = "", List<ImageInfo> oldImagesList = null)
        {
            List<ImageInfo> images = new List<ImageInfo>();
            int counter = 0;
            List<int> notToDelete = new List<int>();
            if (!imageFiles.Any())
            {
                return oldImages;
            }
            foreach (var image in imageFiles)
            {
                if (image.Length == 0)
                {
                    notToDelete.Add(counter);
                    images.Add(new ImageInfo
                    {
                        Name = "Image" + counter,
                        ImagePath = ""
                    });
                    continue;
                }
                counter++;
                var uploadedImageUrl = await ImageService.UploadFile(image);
                images.Add(new ImageInfo
                {
                    Name = "Image" + counter,
                    ImagePath = uploadedImageUrl
                });
            }
            if (isEdit)
            {
                List<ImageInfo> deserializedObject = new List<ImageInfo>();
                if (oldImagesList != null)
                {
                    deserializedObject = oldImagesList;
                }
                else
                {
                    deserializedObject = JsonConvert.DeserializeObject<List<ImageInfo>>(oldImages);
                }
                foreach (var item in deserializedObject)
                {
                    if (notToDelete.Contains(deserializedObject.IndexOf(item)))
                    {
                        continue;
                    }
                    FileManager.DeleteFile(item.ImagePath);
                }

            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(images);
        }
    }



}
