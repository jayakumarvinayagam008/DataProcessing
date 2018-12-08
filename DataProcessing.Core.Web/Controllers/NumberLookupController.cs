using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessing.Core.Web.Controllers
{
    public class NumberLookupController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
        
    }
}