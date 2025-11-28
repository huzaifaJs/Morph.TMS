using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Morpho.Web.Models.Common.Modals
{
    public class ModalHeaderViewModel
    {
        public string Title { get; set; }

        public ModalHeaderViewModel(string title)
        {
            Title = title;
        }
    }

    public class ModalApiBaseUrl
    {
        public string BaseUrl { get; set; }

    }
    public static class FileManagement
    {
        public static async Task<string> WriteFiles(this IFormFile files, string FolderName, string FolderName1)
        {
            bool isSaveSuccess = false;

            string obj = "";
            string final = "";

            string fileName;
            try
            {
                var extension = "." + files.FileName.Split('.')[files.FileName.Split('.').Length - 1];
                fileName = FolderName1 + "_" + DateTime.Now.Ticks + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName, FolderName1);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(pathBuilt));
                }

                var path = Path.Combine("wwwroot", FolderName, FolderName1, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }

                obj = "/" + FolderName + "/" + FolderName1 + "/" + fileName;

                isSaveSuccess = true;
            }
            catch (Exception ex)
            {
                obj = ex.Message;
                return obj;
            }


            return obj;
        }


    }
}
