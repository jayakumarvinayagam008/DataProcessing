using DataProcessing.Application.B2C.Command;
using DataProcessing.Application.B2C.Query;
using DataProcessing.CommonModels;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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

        public BusinessToCustomerController(IOptions<DataProcessingSetting> appSettings,
            ISaveB2C saveB2C,
         IB2CSearchBlock b2CSearchBlock, IB2CSearchAction b2CSearchAction)
        {
            _appSettings = appSettings;
            _saveB2C = saveB2C;
            _b2CSearchBlock = b2CSearchBlock;
            _b2CSearchAction = b2CSearchAction;
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
            _b2CSearchAction.Filter(new B2CSearchFilter
            {
                Area = searchRequest.Area,
                Cities = searchRequest.Cities,
                Contries = searchRequest.Contries,
                States = searchRequest.States,
                Age = searchRequest.Age,
                Experience = searchRequest.Experience,
                Roles = searchRequest.Roles,
                Salary = searchRequest.Salary
            }, "", 100);
            return Json("");
        }

        public ActionResult DownLoadAsExcel(int searchId)
        {
            //fileName = $"{fileName}";
            //var rootPath = _appSettings.Value.SearchExport;
            //var templateName = DownloadTemplateType.GetTemplateName(templateType);
            //var sampleTempate = _getFileContent.Get(fileName, rootPath, templateName);
            //return File(sampleTempate.content, "application/vnd.ms-excel", $"{sampleTempate.TemplateType.Name}.xlsx");
            return Json("***");
        }

        public ActionResult DownLoadAsCsv(int searchId)
        {
            return Json("***");
        }
    }
}