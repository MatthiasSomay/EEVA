﻿using System;
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
    public class AnswerMultipleChoiceController : Controller
    {
        private readonly AnswerManager _answerManager;

        public AnswerMultipleChoiceController(EEVAContext context)
        {
            _answerManager = new AnswerManager(context);
        }

        // GET: AnswerMultipleChoice/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerMultipleChoiceViewModel answerMultipleChoiceViewModel = MapToAnswerMultipleChoiceViewModel(_answerManager.GetMultipleChoice(id));

            if (answerMultipleChoiceViewModel == null)
            {
                return NotFound();
            }

            return View(answerMultipleChoiceViewModel);
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
        public IActionResult Create([Bind("Answer,IsAnswerCorrect,Id")] AnswerMultipleChoiceViewModel answerMultipleChoiceViewModel)
        {
            if (ModelState.IsValid)
            {
                AnswerMultipleChoice answerMultipleChoice = MapToAnswerMultipleChoice(answerMultipleChoiceViewModel);
                _answerManager.Add(answerMultipleChoice);
                return RedirectToAction(nameof(Index));
            }
            return View(answerMultipleChoiceViewModel);
        }

        // GET: AnswerMultipleChoice/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerMultipleChoice answerMultipleChoice = _answerManager.GetMultipleChoice(id);
            if (answerMultipleChoice == null)
            {
                return NotFound();
            }
            else
            {
                AnswerMultipleChoiceViewModel answerMultipleChoiceViewModel = MapToAnswerMultipleChoiceViewModel(answerMultipleChoice);
                return View(answerMultipleChoiceViewModel);
            }
        }

        // POST: AnswerMultipleChoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Answer,IsAnswerCorrect,Id")] AnswerMultipleChoiceViewModel answerMultipleChoiceViewModel)
        {
            if (id != answerMultipleChoiceViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AnswerMultipleChoice answerMultipleChoice = MapToAnswerMultipleChoice(answerMultipleChoiceViewModel);
                    _answerManager.Update(answerMultipleChoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerMultipleChoiceExists(answerMultipleChoiceViewModel.Id))
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

        // GET: AnswerMultipleChoice/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerMultipleChoiceViewModel answerMultipleChoiceViewModel = MapToAnswerMultipleChoiceViewModel(_answerManager.GetMultipleChoice(id));

            if (answerMultipleChoiceViewModel == null)
            {
                return NotFound();
            }

            return View(answerMultipleChoiceViewModel);
        }

        // POST: AnswerMultipleChoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            AnswerMultipleChoice answerMultipleChoice = _answerManager.GetMultipleChoice(id);
            _answerManager.Delete(answerMultipleChoice);
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerMultipleChoiceExists(int id)
        {
            if (_answerManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        //Mapping AnswerMultipleChoice to AnswerMultipleChoiceViewModel
        private AnswerMultipleChoiceViewModel MapToAnswerMultipleChoiceViewModel(AnswerMultipleChoice answerMultipleChoice)
        {
            return new AnswerMultipleChoiceViewModel();
        }

        //Mapping AnswerMultipleChoiceViewModel to AnswerMultipleChoice
        private AnswerMultipleChoice MapToAnswerMultipleChoice(AnswerMultipleChoiceViewModel answerMultipleChoiceViewModel)
        {
            return new AnswerMultipleChoice();
        }

    }
}
