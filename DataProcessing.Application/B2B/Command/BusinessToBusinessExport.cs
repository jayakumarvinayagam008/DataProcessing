using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.B2B.Command
{
    public class BusinessToBusinessExport : IBusinessToBusinessExport
    {
        private readonly ICreateExcel _createExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        private readonly ICreateCsv _createCsv;
        private readonly IBusinessCategoryRepository _businessCategoryRepository;
        private readonly ICreateExcelZip _createExcelZip;
        private readonly ICreateCsvZip _createCsvZip;
        public BusinessToBusinessExport(ICreateExcel createExcel, IDownloadRequestRepository downloadRequestRepository
            , ICreateCsv createCsv, IBusinessCategoryRepository businessCategoryRepository
            , ICreateExcelZip createExcelZip, ICreateCsvZip createCsvZip)
        {
            _createExcel = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
            _createCsv = createCsv;
            _businessCategoryRepository = businessCategoryRepository;
            _createExcelZip = createExcelZip;
            _createCsvZip = createCsvZip;
        }

        public Tuple<string, string> Export(List<BusinessToBusiness> businessToBusinesses, string fileRootPath, int range, int zipFileRange)
        {
            var fileName = GetGUID();
            string fileType = "xlsx";
            string fileCsvType = "csv";
            var filePath = $"{fileRootPath}{fileName}.{fileType}";
            var fileCsvPath = $"{fileRootPath}{fileName}.{fileCsvType}";

            var watch = new System.Diagnostics.Stopwatch();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(businessToBusinesses);
            //JsonConvert.SerializeObject(movie)
            File.WriteAllText($"{fileRootPath}{fileName}.json", jsonString);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

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
            var businessCategoryCall = _businessCategoryRepository.Get();
            businessCategoryCall.Wait();
            var businessCategory = businessCategoryCall.Result;
            /**
             var numbersDetail = numberLookups.GroupJoin(sourceLookUpData,
                x => x.Series,
                y => y.Series,
                  (x, y) => new NumberLookUpDetail
                  {
                      Circle = y.Any() ? y.FirstOrDefault().Circle : string.Empty,
                      Operator = y.Any() ? y.FirstOrDefault().Operator : string.Empty,
                      Phone = x.PhoneNumber
                  }).ToList();

             */
            List<BusinessToBusinessModel> businessToBusinessModels = businessToBusinesses.GroupJoin(businessCategory,
                x => x.CategoryId,
                y => y.CategoryId,
                (x, y) => new BusinessToBusinessModel
                {
                    Add1 = x.Add1,
                    Add2 = x.Add2,
                    Area = x.Area,
                    City = x.City,
                    CompanyName = x.CompanyName,
                    ContactPerson = x.ContactPerson,
                    Contactperson1 = x.Contactperson1,
                    Country = x.Country,
                    Designation = x.Designation,
                    Designation1 = x.Designation1,
                    Email = x.Email,
                    Email1 = x.Email1,
                    EstYear = x.EstYear,
                    Fax = x.Fax,
                    LandMark = x.LandMark,
                    Mobile1 = x.Mobile1,
                    Mobile2 = x.Mobile2,
                    MobileNew = x.Mobile_New,
                    NoOfEmp = x.NoOfEmp,
                    Phone1 = x.Phone1,
                    Phone2 = x.Phone2,
                    PhoneNew = x.Phone_New,
                    Pincode = x.Pincode,
                    State = x.State,
                    Web = x.Web,
                    CategoryName = y.Any() ? y.FirstOrDefault().Name : string.Empty
                }).ToList();

            if(businessToBusinessModels.Count() > zipFileRange)
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
                _createExcelZip.Create(businessToBusinessModels, excelFolder, range, searchRequest[0], zipFileRange);
                //);
                _createCsvZip.Create(businessToBusinessModels, csvFolder, searchRequest[1], zipFileRange);
            }
            else
            {
                _downloadRequestRepository.CreateAsync(searchRequest).Wait();
                Task.Run(() => _createExcel.Create(businessToBusinessModels, filePath, range, searchRequest[0]));
                Task.Run(() => _createCsv.Create(businessToBusinessModels, fileCsvPath, searchRequest[1]));
            }

            return new Tuple<string, string>(searchRequest[0].SearchId, searchRequest[1].SearchId);
        }

        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }
}