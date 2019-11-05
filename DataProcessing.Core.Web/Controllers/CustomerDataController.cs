using DataProcessing.Application.Common;
using DataProcessing.Application.CustomerDate.Command;
using DataProcessing.Application.CustomerDate.Query;
using DataProcessing.CommonModels;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataProcessing.Core.Web.Controllers
{
    [Authorize]
    public class CustomerDataController : Controller
    {
        private readonly IOptions<DataProcessingSetting> _appSettings;
        private readonly ISaveCustomerData _saveCustomerData;
        private readonly ICustomerDataSearchBlock _customerDataSearchBlock;
        private readonly ICustomerDataSearchAction _customerDataSearchAction;
        private readonly IGetSearchedFileStatuscs _getSearchedFileStatuscs;

        public CustomerDataController(IOptions<DataProcessingSetting> appSettings,
            ISaveCustomerData saveCustomerData, ICustomerDataSearchBlock customerDataSearchBlock,
            ICustomerDataSearchAction customerDataSearchAction, IGetSearchedFileStatuscs getSearchedFileStatuscs)
        {
            _appSettings = appSettings;
            _saveCustomerData = saveCustomerData;
            _customerDataSearchBlock = customerDataSearchBlock;
            _customerDataSearchAction = customerDataSearchAction;
            _getSearchedFileStatuscs = getSearchedFileStatuscs;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            var bindResult = _customerDataSearchBlock.BindSearchBlock();
            return View(bindResult);
        }

        [HttpPost]
        public IActionResult Search(SearchRequest searchRequest)
        {
            //Cities| Contrie |States| Network|BusinessVertical| CustomerName|DataQuality
            var searchSummary = _customerDataSearchAction.Filter(new RequestFilter()
            {
                BusinessVertical = searchRequest.BusinessVertical,
                Cities = searchRequest.Cities,
                Contries = searchRequest.Contries,
                DataQuality = searchRequest.DataQuality,
                CustomerName = searchRequest.CustomerName,
                Network = searchRequest.Network,
                States = searchRequest.States,
                Tags = searchRequest.Tags
            },
                _appSettings.Value.SearchExport,
                _appSettings.Value.RowRange, _appSettings.Value.ZipFileRange);
            return Json(searchSummary);
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            CreateUploadFile createUploadFile = new CreateUploadFile();
            var fileCreation = createUploadFile.CreateAsync(files, _appSettings.Value.SharePath);
            fileCreation.Wait();
            var filePath = fileCreation.Result;
            var saveSummary = _saveCustomerData.Save(filePath);
            return View(new UploadSummary()
            {
                ErrorMessage = saveSummary.ErrorMessage,
                TotalCount = saveSummary.TotalCount,
                UploadCount = saveSummary.UploadCount
            });
        }

        public ActionResult DownLoadAsExcel(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.xlsx";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "CustomerData";
            var fileType = Directory.Exists($"{rootPath}{fileName}") ? ".zip" : ".xlsx";
            return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}{fileType}");
        }

        public ActionResult DownLoadAsCsv(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.csv";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "CustomerData";
            var fileType = Directory.Exists($"{rootPath}{fileName}") ? ".zip" : ".csv";
            return File(sampleTempate, "application/x-csv", $"{templateName}{fileType}");
        }

        public ActionResult CheckSearchFileAvailable(SearchRequestCheck searchRequestCheck)
        {
            var fileName = $"{searchRequestCheck.SearchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.{ searchRequestCheck.Type} ";
            var fileType = GetFileType(searchRequestCheck.Type);
            var fileStatus = _getSearchedFileStatuscs.FileExist(searchRequestCheck.SearchId, fileType, filePath);
            return Json(fileStatus);
        }

        private int GetFileType(string fileType)
        {
            return (fileType.ToLower().Equals("xlsx") ? 0 : 1);
        }
    }
}