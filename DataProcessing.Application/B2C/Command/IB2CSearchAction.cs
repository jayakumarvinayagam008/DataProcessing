using DataProcessing.CommonModels;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CSearchAction
    {
        BusinessToCustomerSearchSumary Filter(B2CSearchFilter searchFilter, string rootPath, int range);
    }
}