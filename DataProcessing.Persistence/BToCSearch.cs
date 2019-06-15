using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Persistence
{
    public class BToCSearch
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public List<string> Country { get; set; }
        public List<string> State { get; set; }
        public List<string> City { get; set; }
        public List<string> Area { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Salary { get; set; }
        public List<int> Age { get; set; }
        public List<string> Experience { get; set; }
    }
}
