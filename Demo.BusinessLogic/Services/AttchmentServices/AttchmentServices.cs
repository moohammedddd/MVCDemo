using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttchmentServices
{
   public class AttchmentServices : IAttchmentServices
    {
        List<string> AllowedExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
        int maxSize = 5 * 1024 * 1024; // 5 MB


        #region Upload
        //public string Upload(IFormFile file, string FolderName)
        //{
        //    // 1 Check the extension
        //    if (file is null) return null;
        //    var extension = Path.GetExtension(file.FileName); // png 
        //    if (!AllowedExtensions.Contains(extension)) return null;


        //    // 2Check the file size
        //    if (file.Length > maxSize) return null;
        //    // 3 Get Located Folder Path 
        //    //var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Files",FolderName);
        //    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);

        //    // ✅ Ensure directory exists
        //    if (!Directory.Exists(folderPath))
        //    {
        //        Directory.CreateDirectory(folderPath);
        //    }
        //    // 4 Make Attchment Name Unique 
        //    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        //    //5 Get File Path 
        //    var filePath = Path.Combine(folderPath, fileName);
        //    //6 Create File Stream To Copy File
        //    using var fileStream = new FileStream(filePath, FileMode.Create);
        //    //7 use Stream to copy file 
        //    file.CopyTo(fileStream);
        //    //8 return filename to store in Database 
        //    return fileName;


        //}
        #endregion

        public string? Upload(IFormFile file, string folderName)
        {
            if (file is null) return null;
            //1.Check Extension
            var extension = Path.GetExtension(file.FileName);//.png
            if (!AllowedExtensions.Contains(extension)) return null;
            //2.Check Size
            if (file.Length >= maxSize) return null;

            //3.Get Located Folder Path
            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //6.Create File Stream To Copy File[Unmanaged]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            //7.Use Stream To Copy File
            file.CopyTo(fileStream);
            //8.Return FileName To Store In Database
            return fileName;
        }

        #region Delete
        public bool Delete(string filepath)
        {
            if (!File.Exists(filepath)) return false;
            else
            {
                File.Delete(filepath);
                return true;
            }
        }

        #endregion

        

    }
}
