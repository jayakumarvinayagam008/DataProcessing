﻿using DataProcessing.Application.Home.Queries;
using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DataProcessing.Core.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IGetDashboard _getDashboard;
        private ILogger<HomeController> _log;
        public HomeController(IGetDashboard getDashboard, ILogger<HomeController> log)
        {
            _getDashboard = getDashboard;
            _log = log;
        }

        public IActionResult Index()
        {
            var dashboard = _getDashboard.Get();
            var dashboardSummary = new DashBoardModel()
            {
                BusinessToBusiness = dashboard.BusinessToBusiness,
                BusinessToCustomer = dashboard.BusinessToCustomer,
                CustomerData = dashboard.CustomerData,
                NumberLookup = dashboard.NumberLookup
            };
            return View(dashboardSummary);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}