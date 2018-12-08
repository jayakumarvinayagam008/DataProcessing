using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface ISearchAction
    {
        void Filter(SearchFilter searchFilter);
    }
}
