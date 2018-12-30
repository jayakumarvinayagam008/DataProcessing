using DataProcessing.CommonModels;

namespace DataProcessing.Application.Common
{
    public interface IGetSearchedFileStatuscs
    {
        FileAvailable FileExist(string searchRequistId, int fileType, string filePath);
    }
}