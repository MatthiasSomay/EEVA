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
    public class AnswerMultipleChoiceController : Controller
    {
        private readonly EEVAContext _context;

        public AnswerMultipleChoiceController(EEVAContext context)
        {
            _context = context;
        }

        // GET: AnswerMultipleChoice
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnswersMultipleChoice.ToListAsync());
        }

        // GET: AnswerMultipleChoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerMultipleChoice = await _context.AnswersMultipleChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answerMultipleChoice == null)
            {
                return NotFound();
            }

            return View(answerMultipleChoice);
        }

        // GET: AnswerMultipleChoice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnswerMultipleChoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Answer,IsAnswerCorrect,Id")] AnswerMultipleChoice answerMultipleChoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answerMultipleChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answerMultipleChoice);
        }

        // GET: AnswerMultipleChoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerMultipleChoice = await _context.AnswersMultipleChoice.FindAsync(id);
            if (answerMultipleChoice == null)
            {
                return NotFound();
            }
            return View(answerMultipleChoice);
        }

        // POST: AnswerMultipleChoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Answer,IsAnswerCorrect,Id")] AnswerMultipleChoice answerMultipleChoice)
        {
            if (id != answerMultipleChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerMultipleChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerMultipleChoiceExists(answerMultipleChoice.Id))
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
            return View(answerMultipleChoice);
        }

        // GET: AnswerMultipleChoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerMultipleChoice = await _context.AnswersMultipleChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answerMultipleChoice == null)
            {
                return NotFound();
            }

            return View(answerMultipleChoice);
        }

        // POST: AnswerMultipleChoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerMultipleChoice = await _context.AnswersMultipleChoice.FindAsync(id);
            _context.AnswersMultipleChoice.Remove(answerMultipleChoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerMultipleChoiceExists(int id)
        {
            return _context.AnswersMultipleChoice.Any(e => e.Id == id);
        }
    }
}
