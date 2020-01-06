using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;

namespace EEVA.Web.Controllers
{
    public class QuestionMultipleChoiceController : Controller
    {
        private readonly QuestionManager _questionManager;

        public QuestionMultipleChoiceController(EEVAContext context)
        {
            _questionManager = new QuestionManager(context);
        }
        

        // GET: QuestionMultipleChoice/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionMultipleChoiceViewModel questionMultipleChoiceViewModel = MapToQuestionMultipleChoiceViewModel(_questionManager.GetMultipleChoice(id));

            if (questionMultipleChoiceViewModel == null)
            {
                return NotFound();
            }

            return View(questionMultipleChoiceViewModel);
        }

        // Redirect to Answer Create
        public ActionResult AddAnswer(int? id)
        {
            return RedirectToAction("Create", "AnswerMultipleChoice", new { questionId = id });
        }

        private QuestionMultipleChoiceViewModel MapToQuestionMultipleChoiceViewModel(QuestionMultipleChoice questionMultipleChoice)
        {
            return new QuestionMultipleChoiceViewModel(
                questionMultipleChoice.Id,
                questionMultipleChoice.QuestionPhrase,
                questionMultipleChoice.Answers
                );
        }
    }
}
