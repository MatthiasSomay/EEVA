using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Mvc;
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
