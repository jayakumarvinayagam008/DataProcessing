using DataProcessing.Application.B2B.Command;
using DataProcessing.Application.B2B.Query;
using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace DataProcessing.Core.Web.Controllers
{
    [Authorize]
    public class BusinessToBusinessController : Controller
    {
        private readonly IOptions<DataProcessingSetting> _appSettings;
        private readonly IB2BSearchBlock _searchBlock;
        private readonly ISearchAction _searchAction;
        private readonly IGetSearchedFileStatuscs _getSearchedFileStatuscs;
        private readonly ISaveB2B _saveB2B;
        readonly ILogger<BusinessToBusinessController> _log;
        public BusinessToBusinessController(IOptions<DataProcessingSetting> appSettings,
            ISaveB2B saveB2B, IB2BSearchBlock searchBlock, ISearchAction searchAction,
            IGetSearchedFileStatuscs getSearchedFileStatuscs
            )
        {
            _appSettings = appSettings;
            _saveB2B = saveB2B;
            _searchBlock = searchBlock;
            _searchAction = searchAction;
            _getSearchedFileStatuscs = getSearchedFileStatuscs;
        }

        public IActionResult Index()
        {           
            return View(new B2BDashboard());
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View(new B2BUpload());
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            CreateUploadFile createUploadFile = new CreateUploadFile();
            var fileCreation = createUploadFile.CreateAsync(files, _appSettings.Value.SharePath);
            fileCreation.Wait();
            var filePath = fileCreation.Result;
            var saveSummary = _saveB2B.Save(filePath);
            return View(new B2BUpload()
            {
                ErrorMessage = saveSummary.ErrorMessage,
                TotalCount = saveSummary.TotalCount,
                UploadCount = saveSummary.UploadCount
            });
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Search()
        {
            var searchFilterOption = _searchBlock.BindSearchBlock();
            return View(new B2BSearchModel() { SearchBlock = searchFilterOption });
        }

        [HttpPost]
        public IActionResult Search(SearchRequest searchRequest)
        {
            var searchSummary = _searchAction.Filter(new SearchFilter
            {
                Area = searchRequest.Area,
                BusinessCategoryId = searchRequest.BusinessCategoryId,
                Cities = searchRequest.Cities,
                Contries = searchRequest.Contries,
                Designation = searchRequest.Designation,
                States = searchRequest.States
            }, _appSettings.Value.SearchExport, _appSettings.Value.RowRange, _appSettings.Value.ZipFileRange);
            return Json(searchSummary);
        }

        public ActionResult DownLoadAsExcel(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.DownLoadPath;
            var filePath = $"{rootPath}{fileName}.xlsx";            
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "B2B";
            var fileType = Directory.Exists($"{rootPath}{fileName}") ? ".zip" : ".xlsx";
            return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}{fileType}");
        }

        public ActionResult DownLoadAsCsv(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.DownLoadPath;
            var filePath = $"{rootPath}{fileName}.csv";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "B2B";
            var fileType = Directory.Exists($"{rootPath}{fileName}") ? ".zip" : ".csv";
            return File(sampleTempate, "application/x-csv", $"{templateName}{fileType}");
        }

        public ActionResult CheckSearchFileAvailable(SearchRequestCheck searchRequestCheck)
        {
            var fileName = $"{searchRequestCheck.SearchId}";
            var rootPath = _appSettings.Value.DownLoadPath;
            var filePath = $"{rootPath}{fileName}.{ searchRequestCheck.Type} ";
            var fileType = GetFileType(searchRequestCheck.Type);
            var fileStatus = _getSearchedFileStatuscs.FileExist(searchRequestCheck.SearchId, fileType, filePath);
            return Json(fileStatus);
        }

        public ActionResult DownloadSampleTemplate(int sourceId)
        {            
            var sampleTempate = GetSampleFileContent.Get(_appSettings.Value.SampleDownloadPath, _appSettings.Value.Samples, sourceId);
            return File(sampleTempate.content, "application/vnd.ms-excel", $"{sampleTempate.FileName}.xlsx");
        }

        private int GetFileType(string fileType)
        {
            return (fileType.ToLower().Equals("xlsx") ? 0 : 1);
        }
    }
}