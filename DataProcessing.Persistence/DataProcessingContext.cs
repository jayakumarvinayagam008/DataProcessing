using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataProcessing.Persistence
{
    public class DataProcessingContext : IDataProcessingContext
    {
        private readonly IMongoDatabase _db;
        public DataProcessingContext(IOptions<NoSqlSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        //public DataProcessingContext()
        //{
        //    var client = new MongoClient("mongodb://localhost:27017");
        //    _db = client.GetDatabase("DataProcessing");
        //}
        public IMongoCollection<BusinessToBusiness> BusinessToBusiness => _db.GetCollection<BusinessToBusiness>("BusinessToBusiness");
        public IMongoCollection<BisinessCategory> BisinessCategories => _db.GetCollection<BisinessCategory>("B2BCategory");
        public IMongoCollection<BusinessToCustomer> BusinessToCustomers => _db.GetCollection<BusinessToCustomer>("BusinessToCustomer");
        public IMongoCollection<CustomerData> CustomerDatas => _db.GetCollection<CustomerData>("CustomerData");

        public IMongoCollection<DownloadRequest> DownloadRequests => _db.GetCollection<DownloadRequest>("DownloadRequest");

        public IMongoCollection<SearchHistory> SearchHistories => _db.GetCollection<SearchHistory>("SearchHistory");
    }
}
