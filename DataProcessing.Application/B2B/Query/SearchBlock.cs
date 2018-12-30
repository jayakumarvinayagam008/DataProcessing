using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Query
{
    public class SearchBlock
    {
        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        public int StateId { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }
        public int AreaId { get; set; }
        public IEnumerable<SelectListItem> Area { get; set; }
        public int BusinessCategoryId { get; set; }
        public IEnumerable<SelectListItem> BusinessCategory { get; set; }
        public int DesiginationId { get; set; }
        public IEnumerable<SelectListItem> Desigination { get; set; }
    }
}