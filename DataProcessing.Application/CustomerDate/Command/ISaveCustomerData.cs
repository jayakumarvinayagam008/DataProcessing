using DataProcessing.CommonModels;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ISaveCustomerData
    {
        UploadSummary Save(string filePath);
    }
}