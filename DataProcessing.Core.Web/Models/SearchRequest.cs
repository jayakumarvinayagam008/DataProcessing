using System.Collections.Generic;

namespace DataProcessing.Core.Web.Models
{
    public class SearchRequest
    {
        public IEnumerable<string> Contries { get; set; }
        public IEnumerable<string> States { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<string> Area { get; set; }
        public IEnumerable<string> Designation { get; set; }
        public IEnumerable<int?> BusinessCategoryId { get; set; }

        public IEnumerable<string> BusinessVertical { get; set; }
        public IEnumerable<string> DataQuality { get; set; }
        public IEnumerable<string> Network { get; set; }
        public IEnumerable<string> CustomerName { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}