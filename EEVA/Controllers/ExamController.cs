using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        // GET: Exam
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
            ExamViewModel examViewModel = new ExamViewModel();
            examViewModel.Courses = _courseManager.GetAll();
            examViewModel.Teachers = _contactManager.GetAllTeachers();

            return View(examViewModel);
        }
        
        // POST: Exam/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Date,StartTime,EndTime")] ExamViewModel examViewModel)
        {
            if (ModelState.IsValid)
            {
                Exam exam = new Exam();
                _examManager.Add(exam);
                return RedirectToAction(nameof(Index));
            }
            return View(/*exam*/);
        }

        // GET: Exam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        // POST: Exam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,StartTime,EndTime")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
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
            return View(exam);
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
    }
}
