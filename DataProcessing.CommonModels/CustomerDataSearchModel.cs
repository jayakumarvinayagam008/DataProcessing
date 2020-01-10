using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DataProcessing.CommonModels
{
    public class CustomerDataSearchModel : CommonSearchModel
    {
        public int NetWorkId { get; set; }
        public IEnumerable<SelectListItem> NetWork { get; set; }
        public int BusinessVerticalId { get; set; }
        public IEnumerable<SelectListItem> BusinessVertical { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<SelectListItem> CustomerName { get; set; }
        public int DataQualityId { get; set; }
        public IEnumerable<SelectListItem> DataQuality { get; set; }

        public int IndustryId { get; set; }
        public IEnumerable<SelectListItem> Industries { get; set; }
    }
}