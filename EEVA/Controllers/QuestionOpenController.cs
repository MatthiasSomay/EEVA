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

        public QuestionOpenController(EEVAContext context)
        {
            _questionManager = new QuestionManager(context);
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

        private QuestionOpenViewModel MapToQuestionOpenViewModel(QuestionOpen questionOpen)
        {
            return new QuestionOpenViewModel(
                questionOpen.Id,
                questionOpen.QuestionPhrase,
                questionOpen.Answers
                );
        }
    }
}
