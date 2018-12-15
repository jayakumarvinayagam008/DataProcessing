using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataProcessing.Persistence
{
    public class DownloadRequest
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string SearchId { get; set; }
        public int StatusCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int FileType { get; set; }
    }
}