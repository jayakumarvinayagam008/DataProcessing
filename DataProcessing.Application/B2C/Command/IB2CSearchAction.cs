using System;
using DataProcessing.CommonModels;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CSearchAction
    {
        void Filter(B2CSearchFilter searchFilter);
    }
}
