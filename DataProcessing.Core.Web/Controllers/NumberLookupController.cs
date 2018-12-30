using DataProcessing.Application.NumberLookup.Command;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DataProcessing.Core.Web.Controllers
{
    [Authorize]
    public class NumberLookupController : Controller
    {
        private readonly IOptions<DataProcessingSetting> _appSettings;
        private readonly ILoopupProcess _loopupProcess = null;

        public NumberLookupController(IOptions<DataProcessingSetting> appSettings,
            ILoopupProcess loopupProcess)
        {
            _appSettings = appSettings;
            _loopupProcess = loopupProcess;
        }

        public IActionResult Index()
        {
            NumberLookUpDownload numberLookUpDownload = new NumberLookUpDownload()
            {
            };
            return View(numberLookUpDownload);
        }

        public IActionResult AddLookup()
        {
            NumberLookup numberLookup = new NumberLookup();
            return View(numberLookup);
        }

        [HttpPost]
        public IActionResult SaveLookup(NumberLookup numberLookup)
        {
            numberLookup.IsUploaded = true;
            numberLookup.UploadMessage = "Succesfully uploaded";
            return View("AddLookup", numberLookup);
        }

        [HttpPost]
        public IActionResult BulkLookup(IList<IFormFile> files)
        {
            NumberLookup numberLookup = new NumberLookup() { IsUploaded = true, UploadMessage = "Succesfully uploaded" };
            return View("AddLookup", numberLookup);
        }

        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            CreateUploadFile createUploadFile = new CreateUploadFile();
            var fileCreation = createUploadFile.CreateAsync(files, _appSettings.Value.SharePath);
            fileCreation.Wait();
            var filePath = fileCreation.Result;
            var fileName = _loopupProcess.Process(filePath, _appSettings.Value.NumberLookup);

            NumberLookUpDownload numberLookUpDownload = new NumberLookUpDownload()
            {
                FileName = fileName,
                Status = !string.IsNullOrWhiteSpace(fileName)
            };
            return View(numberLookUpDownload);
        }

        public ActionResult DownLoadNumberLookup(string fileName)
        {
            var rootPath = _appSettings.Value.NumberLookup;
            var filePath = $"{rootPath}{fileName}.xlsx";
            var sampleTempate = new GetFileContent().GetFile(filePath);
            var templateName = "Number LookUp";
            return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}.xlsx");
        }
    }
}