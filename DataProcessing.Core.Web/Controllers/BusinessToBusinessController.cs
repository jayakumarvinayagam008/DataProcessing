using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataProcessing.Application.B2B.Command;
using DataProcessing.Application.B2B.Query;
using DataProcessing.Application.Common;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DataProcessing.Core.Web.Controllers
{
    public class BusinessToBusinessController : Controller
    {
        private readonly IOptions<DataProcessingSetting> _appSettings;
        private readonly IB2BSearchBlock _searchBlock;
        private readonly ISearchAction _searchAction;
        private readonly IGetSearchedFileStatuscs _getSearchedFileStatuscs; 
        private readonly ISaveB2B _saveB2B;

        public BusinessToBusinessController(IOptions<DataProcessingSetting> appSettings,
            ISaveB2B saveB2B, IB2BSearchBlock searchBlock, ISearchAction searchAction,
            IGetSearchedFileStatuscs getSearchedFileStatuscs)
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
            }, _appSettings.Value.SearchExport, _appSettings.Value.RowRange);
            return Json(searchSummary);
        }

        public ActionResult DownLoadAsExcel(string searchId)
        {            
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.xlsx";
            if(_getSearchedFileStatuscs.FileExist(searchId, 0, filePath))
            {
                var sampleTempate = new GetFileContent().GetFile(filePath);
                var templateName = "B2B";
                return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}.xlsx");
            }else
            {
                return Json("Download request in progress!");
            }
           
        }
        public ActionResult DownLoadAsCsv(string searchId)
        {
            var fileName = $"{searchId}";
            var rootPath = _appSettings.Value.SearchExport;
            var filePath = $"{rootPath}{fileName}.csv";
            if (_getSearchedFileStatuscs.FileExist(searchId, 1, filePath))
            {
                var sampleTempate = new GetFileContent().GetFile(filePath);
                var templateName = "B2B";
                return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}.xlsx");
            }
            else
            {
                return Json("Download request in progress!");
            }
        }

    }
}