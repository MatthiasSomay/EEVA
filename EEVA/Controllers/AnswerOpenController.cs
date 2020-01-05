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
    public class AnswerOpenController : Controller
    {
        private readonly AnswerOpenManager _answerOpenManager;

        public AnswerOpenController(EEVAContext context)
        {
            _answerOpenManager = new AnswerOpenManager(context);
        }

        // GET: AnswerOpen/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerOpenViewModel answerOpenViewModel = MapToAnswerOpenViewModel(_answerOpenManager.Get(id));

            if (answerOpenViewModel == null)
            {
                return NotFound();
            }

            return View(answerOpenViewModel);
        }

        // GET: AnswerOpen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnswerOpen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Answer,IsAnswerCorrect,Id")] AnswerOpenViewModel answerOpenViewModel)
        {
            if (ModelState.IsValid)
            {
                AnswerOpen answerOpen = MapToAnswerOpen(answerOpenViewModel);
                _answerOpenManager.Add(answerOpen);
                return RedirectToAction(nameof(Index));
            }
            return View(answerOpenViewModel);
        }

        // GET: AnswerOpen/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerOpen answerOpen = _answerOpenManager.Get(id);
            if (answerOpen == null)
            {
                return NotFound();
            }
            else
            {
                AnswerOpenViewModel answerOpenViewModel = MapToAnswerOpenViewModel(answerOpen);
                return View(answerOpenViewModel);
            }
        }

        // POST: AnswerOpen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Answer,IsAnswerCorrect,Id")] AnswerOpenViewModel answerOpenViewModel)
        {
            if (id != answerOpenViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AnswerOpen answerOpen = MapToAnswerOpen(answerOpenViewModel);
                    _answerOpenManager.Update(answerOpen);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerOpenExists(answerOpenViewModel.Id))
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

        // GET: AnswerOpen/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerOpenViewModel answerOpenViewModel = MapToAnswerOpenViewModel(_answerOpenManager.Get(id));

            if (answerOpenViewModel == null)
            {
                return NotFound();
            }

            return View(answerOpenViewModel);
        }

        // POST: AnswerOpen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            AnswerOpen answerOpen = _answerOpenManager.Get(id);
            _answerOpenManager.Delete(answerOpen);
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerOpenExists(int id)
        {
            if (_answerOpenManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        //Mapping AnswerOpen to AnswerOpenViewModel
        private AnswerOpenViewModel MapToAnswerOpenViewModel(AnswerOpen answerOpen)
        {
            return new AnswerOpenViewModel();
        }

        //Mapping AnswerOpenViewModel to AnswerOpen
        private AnswerOpen MapToAnswerOpen(AnswerOpenViewModel answerOpenViewModel)
        {
            return null;
        }

    }
}
