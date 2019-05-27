using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ISaveBulkNumberLookUp
    {
        void Save(IEnumerable<Numbers> numberLookUpDetails);
    }
}
