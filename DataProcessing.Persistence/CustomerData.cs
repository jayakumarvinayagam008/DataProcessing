using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataProcessing.Persistence
{
    public class CustomerData
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Numbers { get; set; }
        public string Operator { get; set; }
        public string Circle { get; set; }
        public string ClientName { get; set; }
        public string ClientBusinessVertical { get; set; }
        public string Dbquality { get; set; }
        public string DateOfUse { get; set; }
        public string ClientCity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}