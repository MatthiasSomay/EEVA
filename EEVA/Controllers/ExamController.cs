using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EEVA.Web.Controllers
{
    public class ExamController : Controller
    {

        private ExamManager _examManager;

        public ExamController(ExamManager manager)
        {
            _examManager = manager;
        }
        public IActionResult Details(int id)
        {
            Exam e = _examManager.Get(id);
            if(e != null)
            {
                ExamViewModel vm = new ExamViewModel()
                {
                    Id = e.Id,
                    Course = e.Course,
                    Teacher = e.Teacher,
                    Date = e.Date,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    ExamQuestions = e.ExamQuestions,
                    StudentExams = e.StudentExams
                };

                return View(vm);
            }
            else return NotFound();
        }
    }
}