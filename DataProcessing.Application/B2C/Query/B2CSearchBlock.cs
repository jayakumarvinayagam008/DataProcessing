using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DataProcessing.Application.B2C.Query
{
    public class B2CSearchBlock : IB2CSearchBlock
    {
        public readonly IBusinessToCustomerRepository _businessToCustomerRepository;

        public B2CSearchBlock(IBusinessToCustomerRepository businessToCustomerRepository)
        {
            _businessToCustomerRepository = businessToCustomerRepository;
        }

        public B2CSearchBlockModel BindSearchBlock()
        {
            var result = _businessToCustomerRepository.GetFilterBlocks();
            result.Wait();
            var filterOptions = result.Result;
            B2CSearchBlockModel searchBlock = new B2CSearchBlockModel()
            {
                Area = filterOptions.Area.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                City = filterOptions.City.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Country = filterOptions.Country.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Roles = filterOptions.BusinessVertical.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                State = filterOptions.State.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Salaries = filterOptions.Salary.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                //Ages = filterOptions.Age.Select(x => new SelectListItem()
                //{
                //    Value = x,
                //    Text = x
                //}).AsEnumerable(),
                Expercinse = filterOptions.Expercinse.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable()
            };
            return searchBlock;
        }
    }
}