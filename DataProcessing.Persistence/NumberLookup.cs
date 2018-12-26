using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataProcessing.Persistence
{
    public class NumberLookup
    {
        public NumberLookup()
        {
        }
        [BsonId]
        public ObjectId Id { get; set; }
        public string Operator { get; set; }
        public string Circle { get; set; }
        public string Series { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
