using DataProcessing.Application.NumberLookup.Command;
using DataProcessing.Application.NumberLookup.Query;
using DataProcessing.Core.Web.Actions;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Core.Web.Controllers
{
    [Authorize]
    public class NumberLookupController : Controller
    {
        private readonly IOptions<DataProcessingSetting> _appSettings;
        private readonly ILoopupProcess _loopupProcess = null;
        private readonly IReadBulkNumberLookUp _readBulkNumberLookUp = null;
        private readonly IGetOperators _getOperators = null;
        public NumberLookupController(IOptions<DataProcessingSetting> appSettings,
            ILoopupProcess loopupProcess,
            IReadBulkNumberLookUp readBulkNumberLookUp,
            IGetOperators operators)
        {
            _appSettings = appSettings;
            _loopupProcess = loopupProcess;
            _readBulkNumberLookUp = readBulkNumberLookUp;
            _getOperators = operators;
        }

        public IActionResult Index()
        {
            //var getOperators = _getOperators.Get();
            //List<Operator> operators = new List<Operator>();

            //foreach (var item in getOperators)
            //{
            //    operators.Add(new Operator { IsChecked = false, Text = item, Value = item });
            //}

            NumberLookUpDownload numberLookUpDownload = new NumberLookUpDownload()
            {
                //Operators = operators
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
            CreateUploadFile createUploadFile = new CreateUploadFile();
            var fileCreation = createUploadFile.CreateAsync(files, _appSettings.Value.SharePath);
            fileCreation.Wait();
            var filePath = fileCreation.Result;
            _readBulkNumberLookUp.Process(filePath);
            NumberLookup numberLookup = new NumberLookup() { IsUploaded = true, UploadMessage = "Succesfully uploaded" };
            return View("AddLookup", numberLookup);
        }

        [HttpPost]
        public IActionResult Index(IList<IFormFile> files, NumberLookUpDownload noLookUpDownload)
        {
            CreateUploadFile createUploadFile = new CreateUploadFile();
            var fileCreation = createUploadFile.CreateAsync(files, _appSettings.Value.SharePath);
            fileCreation.Wait();
            var filePath = fileCreation.Result;
            //var filters = noLookUpDownload.Operators.Where(x => x.IsChecked).Select(x=>x.Value);
            var searchResult = _loopupProcess.Process(filePath, _appSettings.Value.NumberLookup, noLookUpDownload.LookupNumbers);
            
            List<Operator> operators = new List<Operator>();
            foreach (var item in searchResult.Item1)
            {
                operators.Add(new Operator { IsChecked = false, Text = item, Value = item });
            }
            NumberLookUpDownload numberLookUpDownloadh = new NumberLookUpDownload()
            {
                Operators = operators,
                SearchId = searchResult.Item2
            };
            return View(numberLookUpDownloadh);

            /*var rootPath = _appSettings.Value.NumberLookup;
            var newfilePath = $"{rootPath}{fileName}.xlsx";
            var sampleTempate = new GetFileContent().GetFile(newfilePath);
            var templateName = "Number LookUp";
            return File(sampleTempate, "application/vnd.ms-excel", $"{templateName}.xlsx"); */
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