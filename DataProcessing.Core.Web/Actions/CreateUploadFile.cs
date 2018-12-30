using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Actions
{
    public class CreateUploadFile
    {
        public async Task<string> CreateAsync(IList<IFormFile> files, string rootFilePath)
        {
            var filePath = string.Empty;
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                // full path to file in temp location
                filePath = Path.GetFullPath(rootFilePath);
                // checked file types
                if (fileName.EndsWith(".xlsx") || fileName.EndsWith(".csv"))
                {
                    var dateTime = DateTime.Now;
                    filePath = $"{filePath}\\{GetGUID()}_{fileName}";
                    await SaveFileToServerAsync(filePath);
                    async Task SaveFileToServerAsync(string fileFullPath)
                    {
                        using (var stream = new FileStream(fileFullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
            }
            return filePath;
        }

        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }
}