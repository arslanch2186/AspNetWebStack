using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SellPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Uploads : ControllerBase
    {
        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFile")]
        [AllowAnonymous]
        public async Task<string> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    string basePath = Environment.CurrentDirectory;
                    string UploadFolder = "UploadedFiles";
                    path = Path.GetFullPath(Path.Combine(basePath, UploadFolder));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string filePath = Path.Combine(basePath,UploadFolder, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Path.Combine(UploadFolder,file.FileName);
                }
                else
                {
                    return "No File Selected";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
