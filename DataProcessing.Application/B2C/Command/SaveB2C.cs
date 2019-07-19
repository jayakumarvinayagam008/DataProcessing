using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using MoreLinq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.B2C.Command
{
    public class SaveB2C : ISaveB2C
    {
        private readonly IB2CReadDataFromFile _readDataFromFile;
        private readonly IBusinessToCustomerRepository _businessToCustomerRepository;
        private readonly IUpdateB2CSearchItem _updateB2CSearchItem;
        public SaveB2C(IB2CReadDataFromFile readDataFromFile, IBusinessToCustomerRepository businessToCustomerRepository
            , IUpdateB2CSearchItem updateB2CSearchItem)
        {
            _readDataFromFile = readDataFromFile;
            _businessToCustomerRepository = businessToCustomerRepository;
            _updateB2CSearchItem = updateB2CSearchItem;
        }

        public UploadSummary Save(string filePath)
        {
            var uploadSummary = new UploadSummary();
            var b2CModel = _readDataFromFile.ReadFileData(filePath);
            uploadSummary.TotalCount = b2CModel.Item2;
            //get upload mobileNew value
            var mobileNews = b2CModel.Item1.DistinctBy(x => x.MobileNew);
            string columnvalidationIssuse = "";
            if (b2CModel.Item1.Count() == 0)
                columnvalidationIssuse = " Some column header missing";


            // get mobileNew from datasource
            var b2bDataSource = _businessToCustomerRepository.GetPhoneNewAsync();
            b2bDataSource.Wait();
            var mobileNewRepo = b2bDataSource.Result;
            var businessToCustomer = mobileNews.Except(mobileNewRepo, x => x.PhoneNew, y => y).ToList();
            if (businessToCustomer != null && businessToCustomer.Count()>0)
            {
                var guid = $"b2c-{ Guid.NewGuid()}";
                var saveToSource = businessToCustomer.Select(x => new BusinessToCustomer
                {
                    Address = x.Address,
                    Address2 = x.Address2,
                    AnnualSalary = x.AnnualSalary,
                    Area = x.Area,
                    Caste = x.Caste,
                    City = x.City,
                    Country = x.Country,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    Dob = x.Dob,
                    Email = x.Email,
                    Employer = x.Employer,
                    Experience = x.Experience,
                    Gender = x.Gender,
                    Industry = x.Industry,
                    KeySkills = x.KeySkills,
                    Location = x.Location,
                    Mobile2 = x.Mobile2,
                    MobileNew = x.MobileNew,
                    Name = x.Name,
                    Network = x.Network,
                    PhoneNew = x.PhoneNew,
                    Pincode = x.Pincode,
                    Qualification = x.Qualification,
                    Roles = x.Roles,
                    State = x.State,
                    RefId = guid
                });
                _businessToCustomerRepository.CreateManyAsync(saveToSource);
                uploadSummary.UploadCount = saveToSource.Count();               
                Task task = new Task(() => { _updateB2CSearchItem.Update(guid); });
                task.Start();
            }
            else
            {
                uploadSummary.ErrorMessage = $"Unable to save the upload data due to validation error{columnvalidationIssuse}.";
            }
            return uploadSummary;
        }
    }
}