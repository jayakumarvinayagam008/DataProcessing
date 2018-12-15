using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Controllers
{
    public class GetFileContent
    {
        public byte[] GetFile(string filePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }
        
    }
}
