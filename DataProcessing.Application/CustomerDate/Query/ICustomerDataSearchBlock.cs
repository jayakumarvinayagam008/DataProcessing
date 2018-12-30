using DataProcessing.CommonModels;

namespace DataProcessing.Application.CustomerDate.Query
{
    public interface ICustomerDataSearchBlock
    {
        CustomerDataSearchModel BindSearchBlock();
    }
}