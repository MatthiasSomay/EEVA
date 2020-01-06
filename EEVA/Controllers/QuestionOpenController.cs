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
    public class QuestionOpenController : Controller
    {
        private readonly QuestionManager _questionManager;
        private readonly CourseManager _courseManager;

        public QuestionOpenController(EEVAContext context)
        {
            _questionManager = new QuestionManager(context);
            _courseManager = new CourseManager(context);
        }


        // GET: QuestionOpen/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionOpenViewModel questionOpenViewModel = MapToQuestionOpenViewModel(_questionManager.GetOpen(id));

            if (questionOpenViewModel == null)
            {
                return NotFound();
            }

            return View(questionOpenViewModel);
        }

        // Redirect to Answer Create
        public ActionResult AddAnswer(int? id)
        {
            return RedirectToAction("Create", "AnswerOpen", new { questionId = id });
        }

        // Redirect to Answer Edit
        public ActionResult AnswerEdit(int? id)
        {
            return RedirectToAction("Edit", "AnswerOpen", new { id });
        }

        // Redirect to Answer Details
        public ActionResult AnswerDetails(int? id)
        {
            return RedirectToAction("Details", "AnswerOpen", new { id });
        }

        // Redirect to Answer Delete
        public ActionResult AnswerDelete(int? id)
        {
            return RedirectToAction("Delete", "AnswerOpen", new { id });
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

        //Mapping QuestionOpen to QuestionOpenViewModel
        private QuestionOpenViewModel MapToQuestionOpenViewModel(QuestionOpen questionOpen)
        {
            return new QuestionOpenViewModel(
                questionOpen.Id,
                questionOpen.QuestionPhrase,
                questionOpen.Course,
                questionOpen.Answers,
                _courseManager.GetAll()
                );
        }
    }
}
