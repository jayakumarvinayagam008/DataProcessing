using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProcessing.Application.Common;
using DataProcessing.Application.CustomerDate.Command;
using DataProcessing.Application.CustomerDate.Query;
using DataProcessing.CommonModels;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataProcessing.Core.Web.Controllers
{
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
                _appSettings.Value.RowRange);
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
            return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}.xlsx");
        }
        public ActionResult DownLoadAsCsv(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.csv";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "CustomerData";
            return File(sampleTempate, "application/x-csv", $"{templateName}.csv");
        }

        public ActionResult CheckSearchFileAvailable(SearchRequestCheck searchRequestCheck)
        {
            var fileName = $"{searchRequestCheck.SearchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.{ searchRequestCheck.Type} ";
            var fileStatus = _getSearchedFileStatuscs.FileExist(searchRequestCheck.SearchId, 1, filePath);
            return Json(fileStatus);
        }
    }
}
