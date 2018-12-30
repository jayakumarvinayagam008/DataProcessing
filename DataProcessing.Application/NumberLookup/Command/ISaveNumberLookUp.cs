using DataProcessing.Application.NumberLookup.Query;
using System.Collections.Generic;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ISaveNumberLookUp
    {
        string CreateAndSave(IEnumerable<NumberLookUpDetail> numberLookUpDetails, string rootPath);
    }
}