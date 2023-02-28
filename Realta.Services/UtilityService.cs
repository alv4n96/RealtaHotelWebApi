using Microsoft.AspNetCore.Http;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    public class UtilityService : IUtilityService
    {
        public string UploadSingleFile(IFormFile formFile)
        {
            var fileName = string.Empty;

            try
            {
                var folderName = Path.Combine("Resource", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (formFile.Length>0)
                {
                    fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


            return fileName;
        }
    }
}
