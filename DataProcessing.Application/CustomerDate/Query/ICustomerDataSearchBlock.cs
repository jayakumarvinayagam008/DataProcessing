using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Query
{
    public interface ICustomerDataSearchBlock
    {
        CustomerDataSearchModel BindSearchBlock();
    }
}
