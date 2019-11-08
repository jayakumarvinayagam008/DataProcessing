using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataProcessing.Persistence
{
    public class NumberLookupResult
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Phone { get; set; }
        public string Operator { get; set; }
        public string Circle { get; set; }
        public string Series { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string SearchId { get; set; }
    }
}
