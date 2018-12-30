using System.Collections.Generic;

namespace DataProcessing.Persistence
{
    public class SearchFilterBlock
    {
        public IEnumerable<string> Contries { get; set; }
        public IEnumerable<string> States { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<string> BusinessVertical { get; set; }
        public IEnumerable<string> DataQuality { get; set; }
        public IEnumerable<string> Network { get; set; }
        public IEnumerable<string> ClientName { get; set; }
    }
}