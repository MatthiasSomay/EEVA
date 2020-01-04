using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EEVA.Web.Controllers
{
    public class ExamController : Controller
    {
        private readonly ExamManager _examManager;
        private readonly CourseManager _courseManager;
        private readonly ContactManager _contactManager;
        private readonly QuestionManager _questionManager;

        public ExamController(EEVAContext context)
        {
            _examManager = new ExamManager(context);
            _courseManager = new CourseManager(context);
            _contactManager = new ContactManager(context);
            _questionManager = new QuestionManager(context);
        }

        // GET: Exam
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            List<ExamViewModel> examViewModels = new List<ExamViewModel>();
            IEnumerable<Exam> exams;

            if (!String.IsNullOrEmpty(searchString))
            {
                exams = _examManager.Search(searchString);
            }
            else exams = _examManager.GetAll();

            foreach (Exam e in exams)
            {
                examViewModels.Add(MapToExamViewModel(e));
            }

            int pageSize = 8;
            return View(PaginatedList<ExamViewModel>.Create(examViewModels, pageNumber ?? 1, pageSize));
        }

        // GET: Exam/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExamViewModel examViewModel = MapToExamViewModel(_examManager.Get(id));

            if (examViewModel == null)
            {
                return NotFound();
            }

            return View(examViewModel);
        }

        // GET: Exam/Create
        public IActionResult Create()
        {
            return View(NewExamViewModel());
        }

        // POST: Exam/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Date,StartTime,EndTime,CourseId,TeacherId")] ExamViewModel examViewModel)
        {
            if (ModelState.IsValid)
            {
                Exam exam = MapToExam(examViewModel);
                _examManager.Add(exam);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Exam/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exam exam = _examManager.Get(id);
            if (exam == null)
            {
                return NotFound();
            }
            else
            {
                ExamViewModel examViewModel = MapToExamViewModel(exam);
                return View(examViewModel);
            }
        }

        // POST: Exam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Date,StartTime,EndTime,CourseId,TeacherId")] ExamViewModel examViewModel)
        {
            if (id != examViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Exam exam = MapToExam(examViewModel);
                    _examManager.Update(exam);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(examViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Exam/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ExamViewModel examViewModel = MapToExamViewModel(_examManager.Get(id));

            if (examViewModel == null)
            {
                return NotFound();
            }

            return View(examViewModel);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Exam exam = _examManager.Get(id);
            _examManager.Delete(exam);
            return RedirectToAction(nameof(Index));
        }

        // Redirect to the related details of a Question
        public ActionResult QuestionDetails(int? id)
        {
            return RedirectToAction("Details", "Question", new { id });
        }

        // Redirect to edit Question
        public ActionResult QuestionEdit (int? id)
        {
            return RedirectToAction("Edit", "Question", new { id });
        }

        // Redirect to the related details of a StudentExam
        public ActionResult StudentExamDetails(int? id)
        {
            return RedirectToAction("Edit", "StudentExam", new { id });
        }


        private bool ExamExists(int id)
        {
            if (_examManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }
        
        //Mapping ExamViewModel to Exam
        private Exam MapToExam(ExamViewModel examViewModel)
        {
            return new Exam(
                examViewModel.Id,
                _courseManager.Get(examViewModel.CourseId),
                (Teacher)_contactManager.Get(examViewModel.TeacherId),
                examViewModel.Date,
                examViewModel.StartTime,
                examViewModel.EndTime);       
        }

        //Mapping Exam to ExamViewModel
        private ExamViewModel MapToExamViewModel(Exam exam)
        {
            return new ExamViewModel(
                exam.Id,
                exam.Course,
                exam.Teacher,
                exam.Date,
                exam.StartTime,
                exam.EndTime,
                exam.ExamQuestions,
                exam.StudentExams,
                _courseManager.GetAll(),
                _contactManager.GetAllTeachers()
                );
        }

        //Creating blank ExamViewModel for dropdown list initialization
        private ExamViewModel NewExamViewModel()
        {
            return new ExamViewModel(
                _courseManager.GetAll(),
                _contactManager.GetAllTeachers()
                );
        }
    }
}
