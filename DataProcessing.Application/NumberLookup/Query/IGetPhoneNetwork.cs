using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Query
{
    public interface IGetPhoneNetwork
    {
        IList<NumberLookUpDetail> Get(string searchId, string[] operators);
    }
}
