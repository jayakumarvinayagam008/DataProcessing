using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Models
{
    public class B2BUpload
    {
        public int UploadCount { get; set; }
        public int TotalCount { get; set; }
        public string ErrorMessage { get; set; }        
    }
}
