using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Controllers
{
    public class ExamController : Controller
    {
        private readonly EEVAContext _context;
        private readonly ExamManager _examManager;
        private readonly CourseManager _courseManager;
        private readonly ContactManager _contactManager;

        public ExamController(EEVAContext context)
        {
            _context = context;
            _examManager = new ExamManager(context);
            _courseManager = new CourseManager(context);
            _contactManager = new ContactManager(context);
        }


        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Index()
        {
            return View(_examManager.GetAll());
        }

        // GET: Exam/Details/5
        public IActionResult Details(int? id)
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

            return View(exam);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
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
                exam.Course.Id,
                _contactManager.GetAllTeachers(),
                exam.Teacher.Id
                );
        }

        private ExamViewModel NewExamViewModel()
        {
            return new ExamViewModel(
                _courseManager.GetAll(),
                _contactManager.GetAllTeachers()
                );
        }
    }
}
