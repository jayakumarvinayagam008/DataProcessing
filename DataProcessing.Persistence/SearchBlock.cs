using System;
using System.Collections.Generic;

namespace DataProcessing.Persistence
{
    public class SearchBlock
    {
        public IEnumerable<string> Country { get; set; }
        public IEnumerable<string> State { get; set; }
        public IEnumerable<string> City { get; set; }
        public IEnumerable<string> Area { get; set; }
        public IEnumerable<BusinessCategoryItem> BusinessCategory { get; set; }
        public IEnumerable<string> Desigination { get; set; }
        public IEnumerable<string> BusinessVertical { get; internal set; }
        public IEnumerable<string> Salary { get; internal set; }
        public IEnumerable<string> Expercinse { get; internal set; }
        public IEnumerable<string> Age { get; internal set; }
        public IEnumerable<string> Dbquality { get; internal set; }
        public IEnumerable<string> ClientName { get; internal set; }
        public IEnumerable<string> NetWork { get; internal set; }
    }
}