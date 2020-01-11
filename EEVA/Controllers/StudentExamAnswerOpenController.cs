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
    public class StudentExamAnswerOpenController : Controller
    {
        private readonly EEVAContext _context;

        public StudentExamAnswerOpenController(EEVAContext context)
        {
            _context = context;
        }

        // GET: StudentExamAnswerOpen
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentExamAnswersOpen.ToListAsync());
        }

        // GET: StudentExamAnswerOpen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExamAnswerOpen = await _context.StudentExamAnswersOpen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentExamAnswerOpen == null)
            {
                return NotFound();
            }

            return View(studentExamAnswerOpen);
        }

        // GET: StudentExamAnswerOpen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentExamAnswerOpen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Answer,Id")] StudentExamAnswerOpen studentExamAnswerOpen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentExamAnswerOpen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentExamAnswerOpen);
        }

        // GET: StudentExamAnswerOpen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExamAnswerOpen = await _context.StudentExamAnswersOpen.FindAsync(id);
            if (studentExamAnswerOpen == null)
            {
                return NotFound();
            }
            return View(studentExamAnswerOpen);
        }

        // POST: StudentExamAnswerOpen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Answer,Id")] StudentExamAnswerOpen studentExamAnswerOpen)
        {
            if (id != studentExamAnswerOpen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentExamAnswerOpen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExamAnswerOpenExists(studentExamAnswerOpen.Id))
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
            return View(studentExamAnswerOpen);
        }

        // GET: StudentExamAnswerOpen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExamAnswerOpen = await _context.StudentExamAnswersOpen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentExamAnswerOpen == null)
            {
                return NotFound();
            }

            return View(studentExamAnswerOpen);
        }

        // POST: StudentExamAnswerOpen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentExamAnswerOpen = await _context.StudentExamAnswersOpen.FindAsync(id);
            _context.StudentExamAnswersOpen.Remove(studentExamAnswerOpen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExamAnswerOpenExists(int id)
        {
            return _context.StudentExamAnswersOpen.Any(e => e.Id == id);
        }
    }
}
