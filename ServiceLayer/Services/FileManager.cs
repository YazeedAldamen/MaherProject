using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class FileManager
    {
        public readonly static Dictionary<string, string> ContentTypes = new Dictionary<string, string>
        {
            {"jpg", "image/jpeg"},
            {"gif", "image/gif"},
            {"png", "image/png"},
            {"pdf", "application/pdf"},
            {"doc", "application/msword"},
            {"docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"xls", "application/vnd.ms-excel"},
            {"xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        };

        public static async Task UploadFile(string folderName, string keyName, string? filePath, Stream inputStream)
        {
            try
            {
                var extension = keyName.Substring(keyName.LastIndexOf('.') + 1).ToLower();
                folderName = folderName.Replace("\\", "/");
                folderName = folderName.TrimEnd('/');
                var path = Path.Combine(folderName, keyName);

                ContentTypes.TryGetValue(extension, out string? contentType);

                Directory.CreateDirectory(folderName); // Create the directory if it doesn't exist

                using (FileStream fileStream = File.Create(path))
                {
                    await inputStream.CopyToAsync(fileStream);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while uploading file: " + ex.Message);
            }
        }

        public static void DeleteFile(string fileName)
        {
            try
            {
                string folderName = "wwwroot/images/content/";



                var filePath = Path.Combine(folderName, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                new Exception("Error occurred while deleting file: " + ex.Message);
            }
        }
    }
}
