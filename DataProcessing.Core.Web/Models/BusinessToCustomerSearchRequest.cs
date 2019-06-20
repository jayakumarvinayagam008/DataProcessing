using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Models
{
    public class BusinessToCustomerSearchRequest
    {
        public IEnumerable<string> Contries { get; set; }
        public IEnumerable<string> States { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<string> Area { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> Salary { get; set; }
        public IEnumerable<string> Experience { get; set; }
        public IEnumerable<string> Age { get; set; }
    }
}
