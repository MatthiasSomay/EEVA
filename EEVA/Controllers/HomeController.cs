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
using EEVA.Domain.Models;
using ChartJSCore.Models;
using ChartJSCore.Helpers;

namespace EEVA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CourseManager _courseManager;
        private readonly ContactManager _contactManager;
        

        public List<Course> courses;
       
        public HomeController(EEVAContext context, ILogger<HomeController> logger)
        {
            _courseManager = new CourseManager(context);
            _contactManager = new ContactManager(context);
            _logger = logger;
        }

        public IActionResult Index()
        {

            Chart questionsCourseBarChart = GenerateQuestionsCourseBarChart();
            Chart numberOfCoursesBarChart = GenerateNumberOfCoursesBarChart();

            ViewData["questionsCourseBarChart"] = questionsCourseBarChart;
            ViewData["numberOfCoursesBarChart"] = numberOfCoursesBarChart;


            return View();

        }

        public Chart GenerateNumberOfCoursesBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            Data data = new Data();
            List<Course> courses = _courseManager.GetAll().ToList();

            List<string> course = new List<string>() { "Courses" };

            data.Labels = course;

            List<double> numberOfCourses = new List<double>();
            numberOfCourses.Add(courses.Count());


            BarDataset dataset = new BarDataset()
            {
                Label = "# of Courses",
                Data = numberOfCourses,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 0.2),
                    ChartColor.FromRgba(54, 162, 235, 0.2),
                    ChartColor.FromRgba(255, 206, 86, 0.2),
                    ChartColor.FromRgba(75, 192, 192, 0.2),
                    ChartColor.FromRgba(153, 102, 255, 0.2),
                    ChartColor.FromRgba(255, 159, 64, 0.2)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(255, 99, 132),
                    ChartColor.FromRgb(54, 162, 235),
                    ChartColor.FromRgb(255, 206, 86),
                    ChartColor.FromRgb(75, 192, 192),
                    ChartColor.FromRgb(153, 102, 255),
                    ChartColor.FromRgb(255, 159, 64)
                },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Scales()
            };

            var scales = new Scales
            {
                YAxes = new List<Scale>
                {
                    new CartesianScale
                    {
                        Ticks = new CartesianLinearTick
                        {
                            BeginAtZero = true
                        }
                    }
                },
                XAxes = new List<Scale>
                {
                    new BarScale
                    {
                        BarPercentage = 0.5,
                        BarThickness = 6,
                        MaxBarThickness = 8,
                        MinBarLength = 2,
                        GridLines = new GridLine()
                        {
                            OffsetGridLines = true
                        }
                    }
                }
            };

            options.Scales = scales;

            chart.Options = options;

            chart.Options.Layout = new Layout
            {
                Padding = new Padding
                {
                    PaddingObject = new PaddingObject
                    {
                        Left = 10,
                        Right = 12
                    }
                }
            };

            return chart;

        }

        public Chart GenerateQuestionsCourseBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            Data data = new Data();
            List<Course> courses = _courseManager.GetAll().ToList();
            List<string> courseNames = new List<string>();

            foreach (var item in courses)
            {
                courseNames.Add(item.CourseName);
            }
            data.Labels = courseNames;

            List<double> numberOfQuestions = new List<double>();

            foreach (var item in courses)
            {
                numberOfQuestions.Add(item.Questions.Count());
            }

            BarDataset dataset = new BarDataset()
            {
                Label = "# of Questions",
                Data = numberOfQuestions,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 0.2),
                    ChartColor.FromRgba(54, 162, 235, 0.2),
                    ChartColor.FromRgba(255, 206, 86, 0.2),
                    ChartColor.FromRgba(75, 192, 192, 0.2),
                    ChartColor.FromRgba(153, 102, 255, 0.2),
                    ChartColor.FromRgba(255, 159, 64, 0.2)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(255, 99, 132),
                    ChartColor.FromRgb(54, 162, 235),
                    ChartColor.FromRgb(255, 206, 86),
                    ChartColor.FromRgb(75, 192, 192),
                    ChartColor.FromRgb(153, 102, 255),
                    ChartColor.FromRgb(255, 159, 64)
                },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Scales()
            };

            var scales = new Scales
            {
                YAxes = new List<Scale>
                {
                    new CartesianScale
                    {
                        Ticks = new CartesianLinearTick
                        {
                            BeginAtZero = true
                        }
                    }
                },
                XAxes = new List<Scale>
                {
                    new BarScale
                    {
                        BarPercentage = 0.5,
                        BarThickness = 6,
                        MaxBarThickness = 8,
                        MinBarLength = 2,
                        GridLines = new GridLine()
                        {
                            OffsetGridLines = true
                        }
                    }
                }
            };

            options.Scales = scales;

            chart.Options = options;

            chart.Options.Layout = new Layout
            {
                Padding = new Padding
                {
                    PaddingObject = new PaddingObject
                    {
                        Left = 10,
                        Right = 12
                    }
                }
            };

            return chart;

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
