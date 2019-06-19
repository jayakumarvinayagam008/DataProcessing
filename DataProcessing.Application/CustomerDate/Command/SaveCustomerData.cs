using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using MoreLinq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class SaveCustomerData : ISaveCustomerData
    {
        private readonly ICustomerReadDataFromFile _readDataFromFile;
        private readonly ICustomerDataRepository _businessToCustomerRepository;
        private string createdBy = "Admin";
        private readonly DateTime createdOn;
        private readonly ICustomerDataSearchItem _customerDataSearchItem;

        public SaveCustomerData(ICustomerReadDataFromFile readDataFromFile
            , ICustomerDataRepository businessToCustomerRepository
            , ICustomerDataSearchItem customerDataSearchItem)
        {
            _readDataFromFile = readDataFromFile;
            _businessToCustomerRepository = businessToCustomerRepository;
            createdOn = DateTime.Now;
            _customerDataSearchItem = customerDataSearchItem;
        }

        public UploadSummary Save(string filePath)
        {
            var uploadSummary = new UploadSummary();
            var b2CModel = _readDataFromFile.ReadFileData(filePath);
            uploadSummary.TotalCount = b2CModel.Item2;
            //get upload mobileNew value
            var numbers = b2CModel.Item1.DistinctBy(x => x.Numbers);

            // get mobileNew from datasource
            var b2bDataSource = _businessToCustomerRepository.GetPhoneNewAsync();
            b2bDataSource.Wait();
            var mobileNewRepo = b2bDataSource.Result;
            var customerDate = numbers.Except(mobileNewRepo, x => x.Numbers, y => y).ToList();
            if (customerDate != null)
            {
                var guid = $"cd-{ Guid.NewGuid()}";
                var saveToSource = customerDate.Select(x => new CustomerData
                {
                    Circle = x.Circle,
                    ClientBusinessVertical = x.ClientBusinessVertical,
                    ClientCity = x.ClientCity,
                    ClientName = x.ClientName,
                    Country = x.Country,
                    CreatedBy = createdBy,
                    CreatedDate = createdOn,
                    DateOfUse = x.DateOfUse.ToString(),
                    Dbquality = x.Dbquality,
                    Numbers = x.Numbers,
                    Operator = x.Operator,
                    State = x.State,
                    RefId = guid
                });
                _businessToCustomerRepository.CreateManyAsync(saveToSource);
                uploadSummary.UploadCount = saveToSource.Count();

                Task task = new Task(() => { _customerDataSearchItem.Update(guid); });
                task.Start();
                
            }
            else
            {
                uploadSummary.ErrorMessage = "Unable to save the upload data due to validation error";
            }
            return uploadSummary;
        }
    }
}