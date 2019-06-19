using DataProcessing.Persistence;
using System.Linq;

namespace DataProcessing.Application.B2B.Command
{
    public interface IUpdateB2BSearchItem
    {
        void Update(string refId);
    }

    public class UpdateB2BSearchItem : IUpdateB2BSearchItem
    {
        private readonly IBusinessToBusinessRepository _businessToBusinessRepository;
        private readonly IB2BSearchRepository _searchRepository;

        public UpdateB2BSearchItem(IB2BSearchRepository searchRepository , IBusinessToBusinessRepository businessToBusinessRepository)
        {
            _searchRepository = searchRepository;
            _businessToBusinessRepository = businessToBusinessRepository;
        }
        public void Update(string refId)
        {
            // Get B2B latest insert items
            var latestestUpdate = _businessToBusinessRepository.Filter(refId).Result;
            // Get B2B SearchItems
            var searchItems = _searchRepository.Get().Result.ToList();
            // Country validate and update            
            foreach (var item in searchItems)
            {
                item.Country = item.Country.Union(latestestUpdate.Select(x => x.Country).Distinct()).ToList();
                item.State = item.State.Union(latestestUpdate.Select(x => x.State).Distinct()).ToList();
                item.City = item.City.Union(latestestUpdate.Select(x => x.City).Distinct()).ToList();
                item.Area = item.Area.Union(latestestUpdate.Select(x => x.Area).Distinct()).ToList();
                item.BusinessCategory = item.BusinessCategory.Union(latestestUpdate.Select(x =>(int) x.CategoryId).Distinct()).ToList();
                item.Designation = item.Designation.Union(latestestUpdate.Select(x => x.Designation).Distinct()).ToList();
                _searchRepository.Update(item.Country, item.State, item.City, item.Area, item.Designation, item.BusinessCategory);
            }
        }
    }
}
