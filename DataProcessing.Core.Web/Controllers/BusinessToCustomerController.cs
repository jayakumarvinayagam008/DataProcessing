﻿using DataProcessing.Application.B2C.Command;
using DataProcessing.Application.B2C.Query;
using DataProcessing.Application.Common;
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
    public class BusinessToCustomerController : Controller
    {
        private readonly IOptions<DataProcessingSetting> _appSettings;
        private readonly ISaveB2C _saveB2C;
        private readonly IB2CSearchBlock _b2CSearchBlock;
        private readonly IB2CSearchAction _b2CSearchAction;
        private readonly IGetSearchedFileStatuscs _getSearchedFileStatuscs;
        public BusinessToCustomerController(IOptions<DataProcessingSetting> appSettings,
            ISaveB2C saveB2C,
         IB2CSearchBlock b2CSearchBlock, IB2CSearchAction b2CSearchAction, IGetSearchedFileStatuscs getSearchedFileStatuscs)
        {
            _appSettings = appSettings;
            _saveB2C = saveB2C;
            _b2CSearchBlock = b2CSearchBlock;
            _b2CSearchAction = b2CSearchAction;
            _getSearchedFileStatuscs = getSearchedFileStatuscs;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
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
            var saveSummary = _saveB2C.Save(filePath);
            return View(new UploadSummary()
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
            var searchFilterOption = _b2CSearchBlock.BindSearchBlock();

            return View(searchFilterOption);
        }

        [HttpPost]
        public IActionResult Search(BusinessToCustomerSearchRequest searchRequest)
        {
            var searchSummary = _b2CSearchAction.Filter(new B2CSearchFilter
            {
                Area = searchRequest.Area,
                Cities = searchRequest.Cities,
                Contries = searchRequest.Contries,
                States = searchRequest.States,
                Age = searchRequest.Age,
                Experience = searchRequest.Experience,
                Roles = searchRequest.Roles,
                Salary = searchRequest.Salary
            }, _appSettings.Value.SearchExport, _appSettings.Value.RowRange, _appSettings.Value.ZipFileRange);
            return Json(searchSummary);
        }

        public ActionResult DownLoadAsExcel(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.xlsx";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "B2C";
            var fileType = Directory.Exists($"{rootPath}{fileName}") ? ".zip" : ".xlsx";
            return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}{fileType}");
        }

        public ActionResult DownLoadAsCsv(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.csv";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "B2C";
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