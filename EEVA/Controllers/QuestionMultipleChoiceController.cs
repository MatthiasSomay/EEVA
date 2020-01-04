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
using Microsoft.AspNetCore.Authorization;
using EEVA.Web.Models;

namespace EEVA.Web.Controllers
{
    public class QuestionMultipleChoiceController : Controller
    {
        private readonly QuestionManager _questionManager;

        public QuestionMultipleChoiceController(EEVAContext context)
        {
            _questionManager = new QuestionManager(context);
        }
        /*
        // GET: QuestionMultipleChoice
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
            List<QuestionMultipleChoiceViewModel> questionMultipleChoiceViewModels = new List<QuestionMultipleChoiceViewModel>();
            IEnumerable<QuestionMultipleChoice> questionMultipleChoices;

            if (!String.IsNullOrEmpty(searchString))
            {
                questionMultipleChoices = _questionManager.Search(searchString);
            }
            else questionMultipleChoices = _questionManager.GetAll();

            foreach (Exam e in exams)
            {
                examViewModels.Add(MapToExamViewModel(e));
            }

            int pageSize = 8;
            return View(PaginatedList<ExamViewModel>.Create(examViewModels, pageNumber ?? 1, pageSize));
        }

        // GET: QuestionMultipleChoice/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionMultipleChoice = await _context.QuestionsMultipleChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionMultipleChoice == null)
            {
                return NotFound();
            }

            return View(questionMultipleChoice);
        }

        // GET: QuestionMultipleChoice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionMultipleChoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,QuestionPhrase")] QuestionMultipleChoice questionMultipleChoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionMultipleChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionMultipleChoice);
        }

        // GET: QuestionMultipleChoice/Edit/5
        public IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionMultipleChoice = await _context.QuestionsMultipleChoice.FindAsync(id);
            if (questionMultipleChoice == null)
            {
                return NotFound();
            }
            return View(questionMultipleChoice);
        }

        // POST: QuestionMultipleChoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult> Edit(int id, [Bind("Id,QuestionPhrase")] QuestionMultipleChoice questionMultipleChoice)
        {
            if (id != questionMultipleChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionMultipleChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionMultipleChoiceExists(questionMultipleChoice.Id))
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
            return View(questionMultipleChoice);
        }

        // GET: QuestionMultipleChoice/Delete/5
        public IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionMultipleChoice = await _context.QuestionsMultipleChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionMultipleChoice == null)
            {
                return NotFound();
            }

            return View(questionMultipleChoice);
        }

        // POST: QuestionMultipleChoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult> DeleteConfirmed(int id)
        {
            var questionMultipleChoice = await _context.QuestionsMultipleChoice.FindAsync(id);
            _context.QuestionsMultipleChoice.Remove(questionMultipleChoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionMultipleChoiceExists(int id)
        {
            return _context.QuestionsMultipleChoice.Any(e => e.Id == id);
        }
    }*/
}
