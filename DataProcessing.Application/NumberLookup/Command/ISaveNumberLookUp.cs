using DataProcessing.Application.NumberLookup.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ISaveNumberLookUp
    {
        string CreateAndSave(IEnumerable<NumberLookUpDetail> numberLookUpDetails, string rootPath);
    }
}
