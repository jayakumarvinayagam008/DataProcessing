using System;
using System.Linq;
using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using MoreLinq;

namespace DataProcessing.Application.B2C.Command
{
    public class SaveB2C:ISaveB2C
    {
        private readonly IB2CReadDataFromFile _readDataFromFile;
        private readonly IBusinessToCustomerRepository _businessToCustomerRepository;
        public SaveB2C(IB2CReadDataFromFile readDataFromFile, IBusinessToCustomerRepository businessToCustomerRepository)
        {
            _readDataFromFile = readDataFromFile;
            _businessToCustomerRepository = businessToCustomerRepository;
        }

        public UploadSummary Save(string filePath)
        {
            var uploadSummary = new UploadSummary();
            var b2CModel = _readDataFromFile.ReadFileData(filePath);
            uploadSummary.TotalCount = b2CModel.Item2;
            //get upload mobileNew value
            var mobileNews = b2CModel.Item1.DistinctBy(x => x.MobileNew);

            // get mobileNew from datasource
            var b2bDataSource = _businessToCustomerRepository.GetPhoneNewAsync();
            b2bDataSource.Wait();
            var mobileNewRepo = b2bDataSource.Result;
            var businessToCustomer = mobileNews.Except(mobileNewRepo, x => x.PhoneNew, y => y).ToList();
            if(businessToCustomer != null )
            {
                var saveToSource = businessToCustomer.Select(x => new BusinessToCustomer {
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
                    State = x.State
                });
                _businessToCustomerRepository.CreateManyAsync(saveToSource);
                uploadSummary.UploadCount = saveToSource.Count();
            }
            else
            {
                uploadSummary.ErrorMessage = "Unable to save the upload data due to validation error";
            }
            return uploadSummary;
        }
    }
}
