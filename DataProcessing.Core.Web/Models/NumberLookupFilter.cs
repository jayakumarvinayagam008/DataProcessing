using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Models
{
    public class NumberLookupFilter
    {
        public string LookupId { get; set; }
        public string[] Networks { get; set; }
    }
}
