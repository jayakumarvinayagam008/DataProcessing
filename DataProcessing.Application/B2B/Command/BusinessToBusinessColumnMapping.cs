using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Command
{
    public class BusinessToBusinessColumnMapping
    {
        private Dictionary<string, int> ColumnName { get; set; }

        public BusinessToBusinessColumnMapping()
        {
            ColumnName = new Dictionary<string, int>();
            ColumnName.Add("CompanyName", 1);
            ColumnName.Add("Add1", 2);
            ColumnName.Add("Add2", 3);
            ColumnName.Add("City", 4);
            ColumnName.Add("Area", 5);
            ColumnName.Add("Pincode", 6);
            ColumnName.Add("State", 7);
            ColumnName.Add("Phone_New", 8);
            ColumnName.Add("Mobile_New", 9);
            ColumnName.Add("Phone1", 10);
            ColumnName.Add("Phone2", 11);
            ColumnName.Add("Mobile1", 12);
            ColumnName.Add("Mobile2", 13);
            ColumnName.Add("Fax", 14);
            ColumnName.Add("Email", 15);
            ColumnName.Add("Email1", 16);
            ColumnName.Add("Web", 17);
            ColumnName.Add("ContactPerson", 18);
            ColumnName.Add("Designation", 19);
            ColumnName.Add("Contactperson1", 20);
            ColumnName.Add("Designation1", 21);
            ColumnName.Add("EstYear", 22);
            ColumnName.Add("CategoryId", 23);
            ColumnName.Add("LandMark", 24);
            ColumnName.Add("NoOfEmp", 25);
            ColumnName.Add("Country", 26);
        }

        public Dictionary<string, int> GetCustomerColumnMapping()
        {
            return ColumnName;
        }
    }
}