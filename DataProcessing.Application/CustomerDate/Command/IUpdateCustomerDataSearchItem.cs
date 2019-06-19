using DataProcessing.Persistence;
using System.Linq;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataSearchItem
    {
        void Update(string refId);
    }

    public class CustomerDataSearchItem : ICustomerDataSearchItem
    {
        private readonly ICustomerDataRepository _customerDataRepository;
        private readonly ICustomerDataSearchRepository _searchRepository;
        public CustomerDataSearchItem(ICustomerDataRepository customerDataRepository,
            ICustomerDataSearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
            _customerDataRepository = customerDataRepository;
        }
        public void Update(string refId)
        {
            // Get customer data latest insert items
            var latestestUpdate = _customerDataRepository.Filter(refId).Result;
            // Get customer data SearchItems
            var searchItems = _searchRepository.Get().Result.ToList();
            // Country validate and update            
            foreach (var item in searchItems)
            {
                item.Country = item.Country.Union(latestestUpdate.Select(x => x.Country).Distinct()).ToList();
                item.State = item.State.Union(latestestUpdate.Select(x => x.State).Distinct()).ToList();
                item.City = item.City.Union(latestestUpdate.Select(x => x.ClientCity).Distinct()).ToList();
                item.BusinessVertical = item.BusinessVertical.Union(latestestUpdate.Select(x => x.ClientBusinessVertical).Distinct()).ToList();
                item.Customer = item.Customer.Union(latestestUpdate.Select(x => x.ClientName).Distinct()).ToList();
                item.DataQuality = item.DataQuality.Union(latestestUpdate.Select(x => x.Dbquality).Distinct()).ToList();
                item.Network = item.Network.Union(latestestUpdate.Select(x => x.Operator).Distinct()).ToList();

                _searchRepository.Update(item.Country, item.State, item.City
                    , item.BusinessVertical, item.Network, item.Customer, item.DataQuality );
            }
        }
    }
}
