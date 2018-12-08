﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProcessing.CommonModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataProcessing.Core.Web.Controllers
{
    public class CustomerDataController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View(new CustomerSearchBlock());
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }
    }
}
