using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public class B2BSaveSummary
    {
        public int UploadCount { get; set; }
        public int TotalCount { get; set; }
        public string ErrorMessage { get; set; }
    }
}
