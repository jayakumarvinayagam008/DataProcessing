using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataProcessing.Persistence
{
    public class BusinessToCustomer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Employer { get; set; }
        public string KeySkills { get; set; }
        public string Location { get; set; }
        public string Roles { get; set; }
        public string Industry { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string PhoneNew { get; set; }
        public string MobileNew { get; set; }
        public string Mobile2 { get; set; }
        public string AnnualSalary { get; set; }
        public string Pincode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Network { get; set; }
        public string Gender { get; set; }
        public string Caste { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}