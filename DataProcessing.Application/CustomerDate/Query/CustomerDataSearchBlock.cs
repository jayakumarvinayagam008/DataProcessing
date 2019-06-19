using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DataProcessing.Application.CustomerDate.Query
{
    public class CustomerDataSearchBlock : ICustomerDataSearchBlock
    {
        public readonly ICustomerDataRepository _customerDataRepository;

        public CustomerDataSearchBlock(ICustomerDataRepository customerDataRepository)
        {
            _customerDataRepository = customerDataRepository;
        }

        public CustomerDataSearchModel BindSearchBlock()
        {
            var result = _customerDataRepository.GetFilterBlocks();
            result.Wait();
            var filterOptions = result.Result;

            CustomerDataSearchModel searchBlock = new CustomerDataSearchModel()
            {
                NetWork = filterOptions.NetWork.Select(x => new SelectListItem()
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
                BusinessVertical = filterOptions.BusinessVertical.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                State = filterOptions.State.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                CustomerName = filterOptions.ClientName.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                //DataQuality = filterOptions.Dbquality.Select(x => new SelectListItem()
                //{
                //    Value = x,
                //    Text = x
                //}).AsEnumerable()
            };
            return searchBlock;
        }
    }
}