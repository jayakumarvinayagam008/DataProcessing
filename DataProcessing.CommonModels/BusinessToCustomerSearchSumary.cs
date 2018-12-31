using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.CommonModels
{
    public class BusinessToCustomerSearchSumary
    {
        public decimal Country { get; set; }
        public decimal State { get; set; }
        public decimal City { get; set; }
        public decimal Area { get; set; }
        public decimal Roles { get; set; }
        public decimal Salary { get; set; }
        public decimal Experience { get; set; }
        public decimal Age { get; set; }
        public int SearchCount { get; set; }
        public long Total { get; set; }
        public string SearchId { get; set; }
    }
}
