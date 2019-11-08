using MongoDB.Driver;

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
        IMongoCollection<DataProcessingUser> DataProcessingUsers { get; }
        IMongoCollection<BToBSearch> B2BSearchItems { get; }
        IMongoCollection<BToCSearch> B2CSearchItems { get; }
        IMongoCollection<CustomerDataSearch> UserDataSearchItems { get; }
        IMongoCollection<NumberLookupResult> NumberLookupResult { get; }
    }
}