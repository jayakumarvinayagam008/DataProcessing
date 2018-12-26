using DataProcessing.Application.NumberLookup.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Query
{
    public interface IGetNumberLoopUpData
    {
        IEnumerable<NumberLookUpDetail> FilterNumberLookUp(IEnumerable<Numbers> numberLookups);
    }
}
