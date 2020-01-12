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

namespace EEVA.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionManager _questionManager;
        private readonly CourseManager _courseManager;

        public QuestionController(EEVAContext context)
        {
            _questionManager = new QuestionManager(context);
            _courseManager = new CourseManager(context);
        }

        // GET: Question
        public IActionResult Index(string searchString, string currentFilter, int? pageNumber, bool questionCreated)
        {
            ViewBag.Message = questionCreated;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
            IEnumerable<Question> questions;

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = _questionManager.Search(searchString);
            }
            else questions = _questionManager.GetAll();

            foreach (Question q in questions)
            {
                questionViewModels.Add(MapToQuestionViewModel(q));
            }

            int pageSize = 8;
            return View(PaginatedList<QuestionViewModel>.Create(questionViewModels, pageNumber ?? 1, pageSize));
        }

        // GET: Question/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question question = _questionManager.Get(id);

            if (question == null)
            {
                return NotFound();
            }
            else
            {
                switch (question.GetType().Name.ToString())
                {
                    case "QuestionOpen":
                        return RedirectToAction("Details", "QuestionOpen", new { id });
                    case "QuestionMultipleChoice":
                        return RedirectToAction("Details", "QuestionMultipleChoice", new { id });
                    default:
                        return NotFound();
                }
            }
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            return View(NewQuestionViewModel());
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,QuestionPhrase,CourseId")] QuestionViewModel QuestionViewModel)
        {
            if (ModelState.IsValid)
            {
                Question Question = MapToQuestion(QuestionViewModel);
                _questionManager.Add(Question);
                return RedirectToAction(nameof(Index), new { questionCreated = true });
            }
            return View();
        }

        // GET: Question/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question Question = _questionManager.Get(id);
            if (Question == null)
            {
                return NotFound();
            }
            else
            {
                QuestionViewModel QuestionViewModel = MapToQuestionViewModel(Question);
                return View(QuestionViewModel);
            }
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,QuestionPhrase")] QuestionViewModel QuestionViewModel)
        {
            if (id != QuestionViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Question Question = MapToQuestion(QuestionViewModel);
                    _questionManager.Update(Question);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(QuestionViewModel.Id))
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

        // GET: Question/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionViewModel questionViewModel = MapToQuestionViewModel(_questionManager.Get(id));

            if (questionViewModel == null)
            {
                return NotFound();
            }

            return View(questionViewModel);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Question question = _questionManager.Get(id);
            _questionManager.Delete(question);
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            if (_questionManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        //Mapping Question to QuestionViewModel
        private QuestionViewModel MapToQuestionViewModel(Question question)
        {
            return new QuestionViewModel(
                    question.Id,
                    question.QuestionPhrase,
                    question.Course,
                    _courseManager.GetAll()
            );
        }

        //Mapping Question to QuestionViewModel
        private Question MapToQuestion(QuestionViewModel questionViewModel)
        {
            return new Question(
                questionViewModel.Id,
                questionViewModel.QuestionPhrase,
                _courseManager.Get(questionViewModel.CourseId)
                );
        }

        //Creating blank QuestionViewModel
        private QuestionViewModel NewQuestionViewModel()
        {
            return new QuestionViewModel(_courseManager.GetAll());
        }
    }
}
