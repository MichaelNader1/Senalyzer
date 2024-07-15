using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}"; // Using Guid to ensure unique filenames
            string filePath = Path.Combine(folderPath, fileName);

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

        }





    }
}
