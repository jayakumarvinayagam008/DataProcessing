using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataProcessing.Persistence
{
    public class BusinessToBusiness
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string CompanyName { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string Phone_New { get; set; }
        public string Mobile_New { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email1 { get; set; }
        public string Web { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string Contactperson1 { get; set; }
        public string Designation1 { get; set; }
        public int? EstYear { get; set; }
        public long? CategoryId { get; set; }
        public string LandMark { get; set; }
        public int? NoOfEmp { get; set; }
        public string Country { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string RefId { get; set; }
    }
}