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
    }
}
