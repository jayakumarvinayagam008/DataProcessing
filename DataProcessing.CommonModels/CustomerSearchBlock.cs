using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataProcessing.CommonModels
{
    public class CustomerSearchBlock
    {
        public CustomerSearchBlock()
        {
        }
        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        public int StateId { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }
        public int AreaId { get; set; }
        public IEnumerable<SelectListItem> Area { get; set; }
        public int DesiginationId { get; set; }
        public IEnumerable<SelectListItem> Desigination { get; set; }
    }
}
