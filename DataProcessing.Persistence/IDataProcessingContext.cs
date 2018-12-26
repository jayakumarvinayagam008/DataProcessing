using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Persistence
{
    public interface IDataProcessingContext
    {
        IMongoCollection<BusinessToBusiness> BusinessToBusiness { get; }
        IMongoCollection<BisinessCategory> BisinessCategories { get; }
        IMongoCollection<BusinessToCustomer> BusinessToCustomers { get; }
        IMongoCollection<CustomerData> CustomerDatas { get; }
        IMongoCollection<DownloadRequest> DownloadRequests { get; }
        IMongoCollection<SearchHistory> SearchHistories { get; }
        IMongoCollection<NumberLookup> NumberLookups { get; }
    }
}
