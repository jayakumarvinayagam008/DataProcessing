using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataProcessing.Persistence
{
    public class DataProcessingUser
    {
        public DataProcessingUser()
        {
        }
        [BsonId]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Role { get; set; }
    }
}
