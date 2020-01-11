using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EEVA.Domain;
using EEVA.Domain.Models;

namespace EEVA.Web.Controllers
{
    public class StudentExamAnswerMultipleChoiceController : Controller
    {
        private readonly EEVAContext _context;

        public StudentExamAnswerMultipleChoiceController(EEVAContext context)
        {
            _context = context;
        }

        // GET: StudentExamAnswerMultipleChoice
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentExamAnswersMultipleChoice.ToListAsync());
        }

        // GET: StudentExamAnswerMultipleChoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExamAnswerMultipleChoice = await _context.StudentExamAnswersMultipleChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentExamAnswerMultipleChoice == null)
            {
                return NotFound();
            }

            return View(studentExamAnswerMultipleChoice);
        }

        // GET: StudentExamAnswerMultipleChoice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentExamAnswerMultipleChoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] StudentExamAnswerMultipleChoice studentExamAnswerMultipleChoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentExamAnswerMultipleChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentExamAnswerMultipleChoice);
        }

        // GET: StudentExamAnswerMultipleChoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExamAnswerMultipleChoice = await _context.StudentExamAnswersMultipleChoice.FindAsync(id);
            if (studentExamAnswerMultipleChoice == null)
            {
                return NotFound();
            }
            return View(studentExamAnswerMultipleChoice);
        }

        // POST: StudentExamAnswerMultipleChoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] StudentExamAnswerMultipleChoice studentExamAnswerMultipleChoice)
        {
            if (id != studentExamAnswerMultipleChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentExamAnswerMultipleChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExamAnswerMultipleChoiceExists(studentExamAnswerMultipleChoice.Id))
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
            return View(studentExamAnswerMultipleChoice);
        }

        // GET: StudentExamAnswerMultipleChoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExamAnswerMultipleChoice = await _context.StudentExamAnswersMultipleChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentExamAnswerMultipleChoice == null)
            {
                return NotFound();
            }

            return View(studentExamAnswerMultipleChoice);
        }

        // POST: StudentExamAnswerMultipleChoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentExamAnswerMultipleChoice = await _context.StudentExamAnswersMultipleChoice.FindAsync(id);
            _context.StudentExamAnswersMultipleChoice.Remove(studentExamAnswerMultipleChoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExamAnswerMultipleChoiceExists(int id)
        {
            return _context.StudentExamAnswersMultipleChoice.Any(e => e.Id == id);
        }
    }
}
