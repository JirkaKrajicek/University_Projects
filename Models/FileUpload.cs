using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class FileUpload
    {
        string RootPath;
        string ContentType;
        string FolderName;

        public FileUpload(string rootPath,string folderName, string contentType)
        {
            this.RootPath = rootPath;
            this.FolderName = folderName;
            this.ContentType = contentType;
        }
        public async Task<string> FileUploadAsync(IFormFile iFormFile)
        {
            string filePathOutput = String.Empty;
            var img = iFormFile;

            if (img != null && img.ContentType.ToLower().Contains(ContentType) && img.Length > 0 && img.Length < 2_000_000)
            {
                var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                var fileExtension = Path.GetExtension(img.FileName);
                var FileNameGenerated = Path.GetRandomFileName();

                var fileRelative = Path.Combine(ContentType + "s", FolderName, fileName + fileExtension);
                var filePath = Path.Combine(RootPath, fileRelative);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                filePathOutput = $"/{fileRelative}";
 
            }
            
            return filePathOutput;
        }

        /*public async Task<bool> ProductFileUploadAsync(Product product)
        {
            bool taskSuccess = false;
            var img = product.Image;

            if (img != null && img.ContentType.ToLower().Contains("image") && img.Length > 0 && img.Length < 10_000_000)
            {
                var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                var fileExtension = Path.GetExtension(img.FileName);
                var FileNameGenerated = Path.GetRandomFileName();

                var fileRelative = Path.Combine("images", "Products", fileName + fileExtension);
                var filePath = Path.Combine(RootPath, fileRelative);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                product.ImageSrc = $"/{fileRelative}";
                taskSuccess = true;
            }

            return taskSuccess;
        }*/
    }
}
