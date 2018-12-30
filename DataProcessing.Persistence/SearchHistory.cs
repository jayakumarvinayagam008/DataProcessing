using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataProcessing.Persistence
{
    public class SearchHistory
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string SearchId { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
    }
}