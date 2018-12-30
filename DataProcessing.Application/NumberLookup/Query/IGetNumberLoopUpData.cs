using DataProcessing.Application.NumberLookup.Command;
using System.Collections.Generic;

namespace DataProcessing.Application.NumberLookup.Query
{
    public interface IGetNumberLoopUpData
    {
        IEnumerable<NumberLookUpDetail> FilterNumberLookUp(IEnumerable<Numbers> numberLookups);
    }
}