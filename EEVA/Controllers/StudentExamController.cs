using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EEVA.Domain;
using EEVA.Domain.Models;
using EEVA.Domain.DataManager;
using EEVA.Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EEVA.Web.Controllers
{
    [Authorize(Roles = "Teacher, Admin")]
    public class StudentExamController : Controller
    {
        private readonly StudentExamManager _studentExamManager;
        private readonly ExamManager _examManager;
        private readonly ContactManager _contactManager;

        public StudentExamController(EEVAContext context)
        {
            _studentExamManager = new StudentExamManager(context);
            _examManager = new ExamManager(context);
            _contactManager = new ContactManager(context);
        }

        [Authorize(Roles = "Teacher, Admin, Student")]
        // GET: StudentExam
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
            List<StudentExamViewModel> studentExamViewModels = new List<StudentExamViewModel>();
            IEnumerable<StudentExam> studentExams;

            if (!string.IsNullOrEmpty(searchString))
            {
                studentExams = _studentExamManager.Search(searchString);
            }
            else studentExams = _studentExamManager.GetAll();

            foreach (StudentExam e in studentExams)
            {
                studentExamViewModels.Add(MapToStudentExamViewModel(e));
            }

            int pageSize = 8;
            return View(PaginatedList<StudentExamViewModel>.Create(studentExamViewModels, pageNumber ?? 1, pageSize));
        }


        // GET: StudentExam/Details/5
        public IActionResult Details(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            StudentExamViewModel studentExamViewModel = MapToStudentExamViewModel(_studentExamManager.Get(id));

            if (studentExamViewModel == null)
            {
                return NotFound();
            }

            return View(studentExamViewModel);
        }

        // GET: StudentExam/Create
        public IActionResult Create(int examId, int? pageNumber)
        {
            StudentExam studentExam = NewStudentExam(examId);
            TempData["studentExamId"] = studentExam.Id;
            return RedirectToAction("ExamQuestion", "Question");
        }


        // GET: StudentExam/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentExam studentExam = _studentExamManager.Get(id);
            if (studentExam == null)
            {
                return NotFound();
            }
            else
            {
                StudentExamViewModel studentExamViewModel = MapToStudentExamViewModel(studentExam);
                return View(studentExamViewModel);
            }
        }

        // POST: StudentExam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id")] StudentExamViewModel studentExamViewModel)
        {
            if (id != studentExamViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    StudentExam studentExam = MapToStudentExam(studentExamViewModel);
                    _studentExamManager.Update(studentExam);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExamExists(studentExamViewModel.Id))
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

        // GET: StudentExam/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            StudentExamViewModel studentExamViewModel = MapToStudentExamViewModel(_studentExamManager.Get(id));

            if (studentExamViewModel == null)
            {
                return NotFound();
            }

            return View(studentExamViewModel);
        }

        // POST: StudentExam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            StudentExam studentExam = _studentExamManager.Get(id);
            _studentExamManager.Delete(studentExam);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExamExists(int id)
        {
            if (_studentExamManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        // Mapping StudentExam to StudentExamViewModel
        private StudentExamViewModel MapToStudentExamViewModel(StudentExam studentExam)
        {
            return new StudentExamViewModel(
                studentExam.Id,
                studentExam.Student,
                studentExam.Exam,
                studentExam.StudentExamAnswers
                );
        }

        // Mapping StudentExamViewModel to StudentExam
        private StudentExam MapToStudentExam(StudentExamViewModel studentExamViewModel)
        {
            return new StudentExam(
                studentExamViewModel.Id,
                studentExamViewModel.Student,
                studentExamViewModel.Exam,
                studentExamViewModel.StudentExamAnswers
                );
        }

        private StudentExam NewStudentExam(int examId)
        {
            StudentExam studentExam = new StudentExam(
                (Student)_contactManager.GetByEmail(this.User),
                _examManager.Get(examId)
                );

            _studentExamManager.Add(studentExam);

            return studentExam;
        }
    }
}
