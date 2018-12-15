using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;

namespace DataProcessing.Application.B2B.Command
{
    public enum FileCreateStatus
    {
        Started = 0,
        InProgress = 1,
        Completed = 2
    }
    public static class MessageContainer
    {
        public static string SearchFile = "Download request in progress, please try again.";
    }
    public class BusinessToBusinessExport : IBusinessToBusinessExport
    {
        private readonly ICreateExcel _createExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        private readonly ICreateCsv _createCsv; 
        public BusinessToBusinessExport(ICreateExcel createExcel, IDownloadRequestRepository downloadRequestRepository
            , ICreateCsv createCsv)
        {
            _createExcel = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
            _createCsv = createCsv;
        }
        public string Export(List<BusinessToBusiness> businessToBusinesses, string fileRootPath, int range)
        {
            var fileName = GetGUID();
            string fileType = "xlsx";
            string fileCsvType = "csv";
            var filePath = $"{fileRootPath}{fileName}.{fileType}";
            var fileCsvPath = $"{fileRootPath}{fileName}.{fileCsvType}";
            // db insert
            var createdOn = DateTime.Now;
            var searchRequest = new List<DownloadRequest>() {
                new DownloadRequest
                {
                    CreatedBy = "Admin",
                    CreatedOn = createdOn,
                    SearchId = fileName,
                    StatusCode = (int)FileCreateStatus.Started,
                    FileType = 0 // excel
                },
                new DownloadRequest
                {
                    CreatedBy = "Admin",
                    CreatedOn = createdOn,
                    SearchId = fileName,
                    StatusCode = (int)FileCreateStatus.Started,
                    FileType = 1 // csv
                }
            };
            // get category 
            List<BusinessToBusinessModel> businessToBusinessModels = businessToBusinesses.Select(
                x => new BusinessToBusinessModel
                {
                    Add1 = x.Add1, Add2 = x.Add2, Area = x.Area, City = x.City, CompanyName = x.CompanyName,
                    ContactPerson = x.ContactPerson, Contactperson1 = x.Contactperson1, Country = x.Country,
                    Designation = x.Designation, Designation1 = x.Designation1, Email = x.Email,
                    Email1 = x.Email1, EstYear = x.EstYear, Fax = x.Fax,
                    LandMark = x.LandMark, Mobile1 = x.Mobile1, Mobile2 = x.Mobile2,
                    MobileNew = x.Mobile_New, NoOfEmp = x.NoOfEmp, Phone1 = x.Phone1,
                    Phone2 = x.Phone2, PhoneNew = x.Phone_New, Pincode = x.Pincode, State = x.State, Web = x.Web
                }
                ).ToList();


            _downloadRequestRepository.CreateAsync(searchRequest).Wait();            
            Task.Run(() => _createExcel.Create(businessToBusinessModels, filePath, range, searchRequest[0]));
            Task.Run(() => _createCsv.Create(businessToBusinessModels, fileCsvPath, searchRequest[1]));

            return fileName;
        }

        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }


}
