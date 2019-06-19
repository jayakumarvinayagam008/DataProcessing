using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Persistence
{
    public class CustomerDataSearch
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public List<string> Country { get; set; }
        public List<string> State { get; set; }
        public List<string> City { get; set; }
        public List<string> BusinessVertical { get; set; }
        public List<string> Network { get; set; }
        public List<string> DataQuality { get; set; }
        public List<string> Customer { get; set; }
        public string Label { get; set; }
    }
}
