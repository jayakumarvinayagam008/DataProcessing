using DataProcessing.Persistence;
using System.Linq;

namespace DataProcessing.Application.B2C.Command
{
    public interface IUpdateB2CSearchItem
    {
        void Update(string refId);
    }

    public class UpdateB2CSearchItem : IUpdateB2CSearchItem
    {
        private readonly IBusinessToCustomerRepository _businessToCustomerRepository;
        private readonly IB2CSearchRepository _searchRepository;

        public UpdateB2CSearchItem(IB2CSearchRepository searchRepository, IBusinessToCustomerRepository businessToCustomerRepository)
        {
            _searchRepository = searchRepository;
            _businessToCustomerRepository = businessToCustomerRepository;
        }
        public void Update(string refId)
        {
            // Get B2C latest insert items
            var latestestUpdate = _businessToCustomerRepository.Filter(refId).Result;
            // Get B2C SearchItems
            var searchItems = _searchRepository.Get().Result.ToList();
            // Country validate and update            
            foreach (var item in searchItems)
            {
                item.Country = item.Country.Union(latestestUpdate.Select(x => x.Country).Distinct()).ToList();
                item.State = item.State.Union(latestestUpdate.Select(x => x.State).Distinct()).ToList();
                item.City = item.City.Union(latestestUpdate.Select(x => x.City).Distinct()).ToList();
                item.Area = item.Area.Union(latestestUpdate.Select(x => x.Area).Distinct()).ToList();
                item.Salary = item.Salary.Union(latestestUpdate.Select(x => x.AnnualSalary).Distinct()).ToList();
                item.Experience = item.Salary.Union(latestestUpdate.Select(x => x.Experience).Distinct()).ToList();
                item.Roles = item.Roles.Union(latestestUpdate.Select(x => x.Roles).Distinct()).ToList();
                _searchRepository.Update(item.Country, item.State, item.City, item.Area
                    , item.Roles, item.Salary, item.Experience);
            }
        }
    }
}
