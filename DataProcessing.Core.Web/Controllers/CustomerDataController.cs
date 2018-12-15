using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProcessing.Application.CustomerDate.Command;
using DataProcessing.Application.CustomerDate.Query;
using DataProcessing.CommonModels;
using DataProcessing.Core.Web.Actions;
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
        
        public CustomerDataController(IOptions<DataProcessingSetting> appSettings,
            ISaveCustomerData saveCustomerData, ICustomerDataSearchBlock customerDataSearchBlock)        
        {
            _appSettings = appSettings;
            _saveCustomerData = saveCustomerData;
            _customerDataSearchBlock = customerDataSearchBlock;
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
