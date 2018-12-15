using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.CommonModels
{
    public class ColumnMapping
    {
        private Dictionary<string, int> B2CColumnName { get; set; }

        public ColumnMapping()
        {
            BusinessToCustomerColumnMapping();
            CustomerDataColumnMapping();
        }
        public void BusinessToCustomerColumnMapping()
        {
            B2CColumnName = new Dictionary<string, int>();
            B2CColumnName.Add("Name", 1);
            B2CColumnName.Add("DOB", 2);
            B2CColumnName.Add("Qualification", 3);
            B2CColumnName.Add("Experience", 4);
            B2CColumnName.Add("Employer", 5);
            B2CColumnName.Add("KeySkills", 6);
            B2CColumnName.Add("Location", 7);
            B2CColumnName.Add("Roles", 8);
            B2CColumnName.Add("Industry", 9);
            B2CColumnName.Add("Address", 10);
            B2CColumnName.Add("Address2", 11);
            B2CColumnName.Add("Email", 12);
            B2CColumnName.Add("PhoneNew", 13);
            B2CColumnName.Add("MobileNew", 14);
            B2CColumnName.Add("Mobile2", 15);
            B2CColumnName.Add("AnnualSalary", 16);
            B2CColumnName.Add("Pincode", 17);
            B2CColumnName.Add("Area", 18);
            B2CColumnName.Add("City", 19);
            B2CColumnName.Add("State", 20);
            B2CColumnName.Add("Country", 21);
            B2CColumnName.Add("Network", 22);
            B2CColumnName.Add("Gender", 23);
            B2CColumnName.Add("Caste", 24);
        }

        public Dictionary<string, int> GetB2CColumnMapping()
        {
            return B2CColumnName;
        }

        private Dictionary<string, int> CustomerDataColumn { get; set; }

        public void CustomerDataColumnMapping()
        {
            CustomerDataColumn = new Dictionary<string, int>();
            CustomerDataColumn.Add("Numbers", 1);
            CustomerDataColumn.Add("Operator", 2);
            CustomerDataColumn.Add("Circle", 3);
            CustomerDataColumn.Add("ClientName", 4);
            CustomerDataColumn.Add("ClientBusinessVertical", 5);
            CustomerDataColumn.Add("DbQuality", 6);
            CustomerDataColumn.Add("DateOfUse", 7);
            CustomerDataColumn.Add("ClientCity", 8);
            CustomerDataColumn.Add("State", 9);
            CustomerDataColumn.Add("Country", 10);
        }

        public Dictionary<string, int> GetCustomerColumnMapping()
        {
            return CustomerDataColumn;
        }
    }
}
