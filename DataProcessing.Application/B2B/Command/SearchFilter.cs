using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Command
{
    public class SearchFilter
    {
        public IEnumerable<string> Contries { get; set; }
        public IEnumerable<string> States { get; set; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<string> Area { get; set; }
        public IEnumerable<string> Designation { get; set; }
        public IEnumerable<int?> BusinessCategoryId { get; set; }
    }
}