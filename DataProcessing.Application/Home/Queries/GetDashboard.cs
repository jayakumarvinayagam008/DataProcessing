using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.Home.Queries
{

    public class GetDashboard: IGetDashboard
    {
        private readonly IBusinessToBusinessRepository _businessToBusinessRepository;
        private readonly IBusinessToCustomerRepository _businessToCustomerRepository;
        private readonly ICustomerDataRepository _customerDataRepository;
        private readonly INumberLookupRepository _numberLookupRepository;
        
        public GetDashboard(IBusinessToBusinessRepository businessToBusinessRepository,
            IBusinessToCustomerRepository businessToCustomerRepository,
            ICustomerDataRepository customerDataRepository,
            INumberLookupRepository numberLookupRepository)
        {
            _businessToBusinessRepository = businessToBusinessRepository;
            _businessToCustomerRepository = businessToCustomerRepository;
            _customerDataRepository = customerDataRepository;
            _numberLookupRepository = numberLookupRepository;
        }
        public Dashboard Get()
        {
            Dashboard dashboard = new Dashboard()
            {
                BusinessToBusiness = _businessToBusinessRepository.GetTotalDocument().Result,
                BusinessToCustomer = _businessToCustomerRepository.GetTotalDocument().Result,
                CustomerData = _customerDataRepository.GetTotalDocument().Result,
                NumberLookup = _numberLookupRepository.GetTotalDocument().Result
            };
            return dashboard;
        }
    }
}
