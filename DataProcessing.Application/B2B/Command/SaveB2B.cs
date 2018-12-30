using DataProcessing.Application.Common;
using DataProcessing.Persistence;
using MoreLinq;
using System.Linq;

namespace DataProcessing.Application.B2B.Command
{
    public class SaveB2B : ISaveB2B
    {
        private readonly IReadDataFromFile _readDataFromFile;
        public readonly IBusinessToBusinessRepository _businessToBusinessRepository;

        public SaveB2B(IReadDataFromFile readDataFromFile, IBusinessToBusinessRepository businessToBusinessRepository)
        {
            _readDataFromFile = readDataFromFile;
            _businessToBusinessRepository = businessToBusinessRepository;
        }

        public B2BSaveSummary Save(string filePath)
        {
            //var response = new B2BSaveSummary()
            //{
            //    ErrorMessage = "********* **************",
            //    TotalCount = 5000,
            //    UploadCount = 4500
            //};
            //return response;
            var response = new B2BSaveSummary();
            var b2bModel = _readDataFromFile.ReadFileData(filePath);
            // remove dublicate
            var businessToBusiness = b2bModel.Item1.DistinctBy(x => x.PhoneNew);
            response.UploadCount = b2bModel.Item2;

            var b2bDataSource = _businessToBusinessRepository.GetAllB2BAsync();
            b2bDataSource.Wait();

            var b2bDataRepo = b2bDataSource.Result;
            var phoneNew = b2bDataRepo.Select(x => x.Phone_New).ToList();
            businessToBusiness = businessToBusiness.Except(phoneNew, x => x.PhoneNew, y => y).ToList();
            if (businessToBusiness != null)
            {
                var saveToSource = businessToBusiness.Select(x => new BusinessToBusiness
                {
                    Add1 = x.Add1,
                    Add2 = x.Add2,
                    Area = x.Area,
                    CategoryId = x.CategoryId,
                    City = x.City,
                    CompanyName = x.CompanyName,
                    ContactPerson = x.ContactPerson,
                    Contactperson1 = x.Contactperson1,
                    Country = x.Country,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = $"{x.CreatedDate}",
                    Designation = x.Designation,
                    Designation1 = x.Designation1,
                    Email = x.Email,
                    Email1 = x.Email1,
                    EstYear = x.EstYear,
                    Fax = x.Fax,
                    LandMark = x.LandMark,
                    Mobile1 = x.Mobile1,
                    Mobile2 = x.Mobile2,
                    Mobile_New = x.Mobile2,
                    NoOfEmp = x.NoOfEmp,
                    Phone1 = x.Phone1,
                    Phone2 = x.Phone2,
                    Phone_New = x.PhoneNew,
                    Pincode = x.Pincode,
                    State = x.State,
                    Web = x.Web
                }).ToList();
                _businessToBusinessRepository.CreateManyAsync(saveToSource);
                response.TotalCount = saveToSource.Count();
            }
            else
            {
                response.ErrorMessage = "Unable to save the upload data due to validation error";
            }

            return response;
        }
    }
}