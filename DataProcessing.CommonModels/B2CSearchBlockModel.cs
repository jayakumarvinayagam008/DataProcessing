using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataProcessing.CommonModels
{
    public class B2CSearchBlockModel
    {
        public B2CSearchBlockModel()
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

        public int RoleId { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public int SalaryId { get; set; }
        public IEnumerable<SelectListItem> Salaries { get; set; }

        public int AgeId { get; set; }
        public IEnumerable<SelectListItem> Ages { get; set; }

        public int ExpercinseId { get; set; }
        public IEnumerable<SelectListItem> Expercinse { get; set; }
    }
}
