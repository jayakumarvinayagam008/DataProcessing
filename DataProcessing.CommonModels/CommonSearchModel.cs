using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DataProcessing.CommonModels
{
    public class CommonSearchModel
    {
        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        public int StateId { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }
        public int AreaId { get; set; }
        public IEnumerable<SelectListItem> Area { get; set; }
    }
}