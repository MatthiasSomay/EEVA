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

        // Redirect to Answer Edit
        public ActionResult AnswerEdit(int? id)
        {
            return RedirectToAction("Edit", "AnswerMultipleChoice", new { id });
        }

        // Redirect to Answer Details
        public ActionResult AnswerDetails(int? id)
        {
            return RedirectToAction("Details", "AnswerMultipleChoice", new { id });
        }

        // Redirect to Answer Delete
        public ActionResult AnswerDelete(int? id)
        {
            return RedirectToAction("Delete", "AnswerMultipleChoice", new { id });
        }

        // Redirect to Question Index
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Index", "Question", new { id });
        }

        // Redirect to Question Edit
        public ActionResult Edit(int? id)
        {
            return RedirectToAction("Edit", "Question", new { id });
        }

        //Mapping QuestionMultipleChoice to QuestionMultipleChoiceViewModel
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
