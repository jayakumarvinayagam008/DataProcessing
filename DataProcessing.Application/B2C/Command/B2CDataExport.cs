using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;

namespace DataProcessing.Application.B2C.Command
{
    public class B2CDataExport : IB2CDataExport
    {
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        private readonly IB2CCsvExport _b2CCsvExport;
        private readonly IB2CExcelExport _b2CExcelExport;
        private readonly IB2CreateExcelZip _createExcelZip;
        private readonly IB2CreateCsvZip _createCsvZip;
        public B2CDataExport(IDownloadRequestRepository downloadRequestRepository, IB2CCsvExport b2CCsvExport
            , IB2CExcelExport b2CExcelExport, IB2CreateExcelZip createExcelZip, IB2CreateCsvZip createCsvZip)
        {
            _downloadRequestRepository = downloadRequestRepository;
            _b2CCsvExport = b2CCsvExport;
            _b2CExcelExport = b2CExcelExport;
            _createExcelZip = createExcelZip;
            _createCsvZip = createCsvZip;
        }
        public Tuple<string, string> Export(List<BusinessToCustomer> businessToBusinesses, string fileRootPath, int range, int zipFileRange = 0)
        {
            var fileName = GetGuid.Get();
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
            //
            var download = businessToBusinesses.Select(x => new B2CSearchResult
            {
                Address = x.Address,
                Address2 = x.Address2,
                AnnualSalary = x.AnnualSalary,
                Area = x.Area,
                Caste = x.Caste,
                City = x.City,
                Country = x.Country,
                Dob = (x.Dob.HasValue) ? x.Dob.Value.ToString("MM/dd/yyyy") : string.Empty,
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
            }).ToList();

            if(download.Count() > zipFileRange)
            {
                var folderPath = $"{fileRootPath}{fileName}";
                var excelFolder = $"{folderPath}{"Excel"}";
                var csvFolder = $"{folderPath}{"Csv"}";

                searchRequest[0].SearchId = $"{searchRequest[0].SearchId}{"Excel"}";
                searchRequest[1].SearchId = $"{searchRequest[1].SearchId}{"Csv"}";
                _downloadRequestRepository.CreateAsync(searchRequest).Wait();

                try
                {
                    // If the directory doesn't exist, create it.

                    if (!Directory.Exists(excelFolder))
                    {
                        Directory.CreateDirectory(excelFolder);
                    }

                    if (!Directory.Exists(csvFolder))
                    {
                        Directory.CreateDirectory(csvFolder);
                    }
                }
                catch (Exception)
                {
                    // Fail silently
                }
                //Task.Run(() =>
                _createExcelZip.Create(download, excelFolder, range, searchRequest[0], zipFileRange);
                //);
                _createCsvZip.Create(download, csvFolder, searchRequest[1], zipFileRange);

            }
            else
            {
                _downloadRequestRepository.CreateAsync(searchRequest).Wait();
                Task.Run(() => _b2CExcelExport.Create(download, filePath, range, searchRequest[0]));
                Task.Run(() => _b2CCsvExport.Create(download, fileCsvPath, searchRequest[1]));
            }

           

            return new Tuple<string, string>(searchRequest[0].SearchId, searchRequest[1].SearchId);
        }
    }
}
