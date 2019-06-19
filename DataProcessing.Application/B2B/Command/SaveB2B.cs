using DataProcessing.Application.Common;
using DataProcessing.Persistence;
using MoreLinq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.B2B.Command
{
    public class SaveB2B : ISaveB2B
    {
        private readonly IReadDataFromFile _readDataFromFile;
        public readonly IBusinessToBusinessRepository _businessToBusinessRepository;
        private readonly IUpdateB2BSearchItem _updateB2BSearchItem;

        public SaveB2B(IReadDataFromFile readDataFromFile, 
            IBusinessToBusinessRepository businessToBusinessRepository,
            IUpdateB2BSearchItem updateB2BSearchItem)
        {
            _readDataFromFile = readDataFromFile;
            _businessToBusinessRepository = businessToBusinessRepository;
            _updateB2BSearchItem = updateB2BSearchItem;
        }

        public B2BSaveSummary Save(string filePath)
        {       
            //_updateB2BSearchItem.Update("b2b-7bc2c8f0-8e1f-406c-bd88-f1a64c3ee0f7");  
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
            if (businessToBusiness != null && businessToBusiness.Count()>0)
            {
                var guid = $"b2b-{ Guid.NewGuid()}";
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
                    Web = x.Web,
                    RefId = guid
                }).ToList();

                var fromB2B = _businessToBusinessRepository.CreateManyAsync(saveToSource).Result;
                response.TotalCount = saveToSource.Count();
                // call filter integration
                //_updateB2BSearchItem.Update(guid);  
                Task task = new Task(() => { _updateB2BSearchItem.Update(guid); });
                task.Start();
            }
            else
            {
                if(businessToBusiness.Count() == 0)
                    response.ErrorMessage = "Document save count is 0";
                else
                    response.ErrorMessage = "Unable to save the upload data due to validation error";
            }

            return response;
        }
    }
}