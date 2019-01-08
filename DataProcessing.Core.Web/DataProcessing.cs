using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web
{
    public class DataProcessingSetting
    {
        public string SharePath { get; set; }
        public string SampleDownloadPath { get; set; }
        public string NumberLookup { get; set; }
        public string SearchExport { get; set; }
        public int RowRange { get; set; }
        public string[] Samples { get; set; }
    }
}
