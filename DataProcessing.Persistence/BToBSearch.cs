using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataProcessing.Persistence
{
    public class BToBSearch
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public List<string> Country { get; set; }
        public List<string> State { get; set; }
        public List<string> City { get; set; }
        public List<string> Area { get; set; }
        public List<int> BusinessCategory { get; set; }
        public List<string> Designation { get; set; }
    }
}
