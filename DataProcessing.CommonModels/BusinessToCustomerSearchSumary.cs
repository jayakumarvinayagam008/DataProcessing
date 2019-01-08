using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.CommonModels
{
    public class BusinessToCustomerSearchSumary
    {
        public decimal Name { get; set; }
        public decimal Dob { get; set; }
        public decimal Qualification { get; set; }
        public decimal Experience { get; set; }
        public decimal Employer { get; set; }
        public decimal KeySkills { get; set; }
        public decimal Location { get; set; }
        public decimal Roles { get; set; }
        public decimal Industry { get; set; }
        public decimal Address { get; set; }
        public decimal Address2 { get; set; }
        public decimal Email { get; set; }
        public decimal PhoneNew { get; set; }
        public decimal MobileNew { get; set; }
        public decimal Mobile2 { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal Pincode { get; set; }
        public decimal Area { get; set; }
        public decimal City { get; set; }
        public decimal State { get; set; }
        public decimal Country { get; set; }
        public decimal Network { get; set; }
        public decimal Gender { get; set; }
        public decimal Caste { get; set; }

        public int SearchCount { get; set; }
        public long Total { get; set; }
        public string SearchId { get; set; }
    }
}
