using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IDownloadRequestRepository
    {
        Task<DownloadRequest> GetDownloadRequestDetail(string requestId, int typeId);

        Task CreateAsync(List<DownloadRequest> downloadRequest);

        Task<bool> UpdateAsync(DownloadRequest downloadRequest);
    }

    public class DownloadRequestRepository : IDownloadRequestRepository
    {
        private readonly IDataProcessingContext _context;

        public DownloadRequestRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(List<DownloadRequest> downloadRequest)
        {
            await _context.DownloadRequests.InsertManyAsync(downloadRequest);
        }

        public Task<DownloadRequest> GetDownloadRequestDetail(string requestId, int typeId)
        {
            FilterDefinition<DownloadRequest> filter = Builders<DownloadRequest>.Filter.Eq(m => m.SearchId, requestId)
                & Builders<DownloadRequest>.Filter.Eq(m => m.FileType, typeId);

            return _context
                    .DownloadRequests
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(DownloadRequest downloadRequest)
        {
            ReplaceOneResult updateResult =
            await _context
                    .DownloadRequests
                    .ReplaceOneAsync(
                        filter: g => (g.SearchId == downloadRequest.SearchId && g.FileType == downloadRequest.FileType),
                        replacement: downloadRequest);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}