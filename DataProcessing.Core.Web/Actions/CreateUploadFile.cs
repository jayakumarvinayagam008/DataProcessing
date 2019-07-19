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
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Replace(" ", "").Trim('"');
                // full path to file in temp location
                filePath = Path.GetFullPath(rootFilePath);
                FileInfo fi = new FileInfo(fileName);
                // checked file types
                if (fileName.EndsWith(".xlsx") || fileName.EndsWith(".csv"))
                {
                    var dateTime = DateTime.Now;
                    filePath = $"{filePath}\\{GetGUID()}{fi.Extension}";
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