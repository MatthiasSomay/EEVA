using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EEVA.Models;
using EEVA.Domain.DataManager;
using EEVA.Domain;
using EEVA.Web.Models;
using static EEVA.Web.Models.Chart;

namespace EEVA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ContactManager _contactManager;



        public HomeController(EEVAContext context, ILogger<HomeController> logger)
        {
            _contactManager = new ContactManager(context);
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();

        }
        public JsonResult BarChartData()
        {
            Chart _chart = new Chart();
            _chart.labels = new string[] { "Jan", "Feb", "Mar" };
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "This Year",
                data = new int[] { 40, 60, 20 },
                backgroundColor = new string[] { "800000", "#E9967C", "#FF0000" },
                borderColor = new string[] { "800000", "#E9967C", "#FF0000" },
                borderWidth = "1"
            });
            _chart.datasets = _dataSet;
            return Json(_chart);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Exam()
        {
            return View();
        }

        public IActionResult Course()
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
