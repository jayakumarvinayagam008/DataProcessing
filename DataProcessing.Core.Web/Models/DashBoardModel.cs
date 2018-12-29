using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Models
{
    public class DashBoardModel
    {
        public long BusinessToBusiness { get; set; }
        public long BusinessToCustomer { get; set; }
        public long CustomerData { get; set; }
        public long NumberLookup { get; set; }
    }
}
