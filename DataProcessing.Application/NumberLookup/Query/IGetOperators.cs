using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Query
{
    public interface IGetOperators
    {
        IEnumerable<string> Get();
    }
}
