using System.Collections.Generic;

namespace DataProcessing.Core.Web.Models
{
    public class NumberLookUpDownload
    {
        public string FileName { get; set; }
        public bool Status { get; set; }
        public List<Operator> Operators { get; set; }
        public string LookupNumbers { get; set; }
        public string SearchId { get; set; }
    }
}