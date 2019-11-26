using DataProcessing.Application.NumberLookup.Command;
using System.Collections.Generic;

namespace DataProcessing.Application.NumberLookup.Query
{
    public interface IGetNumberLookUpData
    {
        IEnumerable<NumberLookUpDetail> FilterNumberLookUp(IEnumerable<Numbers> numberLookups);
        IEnumerable<NumberLookUpDetail> GetNumberLookUp();
    }
}