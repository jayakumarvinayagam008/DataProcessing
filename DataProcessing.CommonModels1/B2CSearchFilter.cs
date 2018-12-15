using System;
using System.Collections.Generic;

namespace DataProcessing.CommonModels
{
    public class B2CSearchFilter
    {
        public B2CSearchFilter()
        {
        }
        public IEnumerable<string> Contries { get; set; }
        public IEnumerable<string> States { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<string> Area { get; set; }
        public IEnumerable<string> Designation { get; set; }
    }
}
