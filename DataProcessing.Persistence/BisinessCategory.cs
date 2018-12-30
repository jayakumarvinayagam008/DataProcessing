using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataProcessing.Persistence
{
    public class BisinessCategory
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}