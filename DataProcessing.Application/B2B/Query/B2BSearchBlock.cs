using DataProcessing.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.B2B.Query
{
    public class B2BSearchBlock : IB2BSearchBlock
    {
        public readonly IBusinessToBusinessRepository _businessToBusinessRepository;
        public B2BSearchBlock(IBusinessToBusinessRepository businessToBusinessRepository)
        {
            _businessToBusinessRepository = businessToBusinessRepository;
        }
        public SearchBlock BindSearchBlock()
        {
            //var result = _businessToBusinessRepository.GetFilterBlocks();
            //result.Wait();
            //var filterOptions = result.Result;
            //SearchBlock searchBlock = new SearchBlock()
            //{
            //    Area = filterOptions.Area.Select(x => new SelectListItem()
            //    {
            //        Value = x,
            //        Text = x
            //    }).AsEnumerable(),
            //    BusinessCategory = filterOptions.BusinessCategory.Select(x => new SelectListItem()
            //    {
            //        Value =$"{ x.Id }",
            //        Text = x.Name
            //    }).AsEnumerable(),
            //    City = filterOptions.City.Select(x => new SelectListItem()
            //    {
            //        Value = x,
            //        Text = x
            //    }).AsEnumerable(),
            //    Country = filterOptions.Country.Select(x => new SelectListItem()
            //    {
            //        Value = x,
            //        Text = x
            //    }).AsEnumerable(),
            //    Desigination = filterOptions.Desigination.Select(x => new SelectListItem()
            //    {
            //        Value = x,
            //        Text = x
            //    }).AsEnumerable(),
            //    State = filterOptions.State.Select(x => new SelectListItem()
            //    {
            //        Value = x,
            //        Text = x
            //    }).AsEnumerable()
            //};
            var country = new string[] { "India" };
            var state = new string[] { "TamilNadu" };
            var city = new string[] { "Chennai" };
            var area = new string[] { "Kodampakkam" };
            var desigination = new string[] { "Developer" };
            var category = new string[] { "AAAAAAAA", "CCCCCCC", "DDDDDDDDD" };
            SearchBlock searchBlock = new SearchBlock()
            {
                Area = area.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                BusinessCategory = category.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                City = city.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Country = country.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Desigination = desigination.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                State = state.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable()
            };

            return searchBlock;
        }
    }
}
