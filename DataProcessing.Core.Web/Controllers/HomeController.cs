using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataProcessing.Core.Web.Models;
using DataProcessing.Application.Home.Queries;

namespace DataProcessing.Core.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetDashboard _getDashboard;
        public HomeController(IGetDashboard getDashboard)
        {
            _getDashboard = getDashboard;
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
