using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using EEVA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EEVA.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ContactManager _contactManager;
        private readonly ExamManager _examManager;


        public TeacherController(EEVAContext context)
        {
            _contactManager = new ContactManager(context);
            _examManager = new ExamManager(context);
        }


        [Authorize(Roles = "Teacher, Admin")]
        public ActionResult Index(string searchString, string currentFilter, int? pageNumber, string message)
        {
            ViewBag.Message = message;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            List<TeacherViewModel> teacherViewModels = new List<TeacherViewModel>();
            IEnumerable<Contact> teachers;

            if (!string.IsNullOrEmpty(searchString))
            {
                teachers = _contactManager.Search(searchString);
            }
            else teachers = _contactManager.GetAll();

            foreach (Contact c in teachers)
            {
                if (c is Teacher)
                {
                    Teacher teacher = (Teacher) c;
                    teacherViewModels.Add(MapToTeacherViewModel(teacher));
                }

            }

            int pageSize = 8;
            return View(PaginatedList<TeacherViewModel>.Create(teacherViewModels, pageNumber ?? 1, pageSize));
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            TeacherViewModel teacherViewModel = MapToTeacherViewModel((Teacher) _contactManager.Get(id));

            if (teacherViewModel == null)
            {
                return NotFound();
            }

            return View(teacherViewModel);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return View(new TeacherViewModel());
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber")] TeacherViewModel teacherViewModel)
        {
            if(ModelState.IsValid)
            {
                Teacher teacher = MapToTeacher(teacherViewModel);
                _contactManager.Add(teacher);
                return RedirectToAction(nameof(Index), new { message = "create"});
            }
            
                return View();
            
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Teacher teacher = (Teacher) _contactManager.Get(id);
            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                TeacherViewModel teacherViewModel = MapToTeacherViewModel(teacher);
                return View(teacherViewModel);
            }
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeacherViewModel teacherViewModel)
        {
            if (id != teacherViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Teacher teacher = MapToTeacher(teacherViewModel);
                    _contactManager.Update(teacher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacherViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { message = "edit"});
            }
            return View();
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            TeacherViewModel teacherViewModel = MapToTeacherViewModel( (Teacher) _contactManager.Get(id));

            if (teacherViewModel == null)
            {
                return NotFound();
            }

            return View(teacherViewModel);
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Teacher teacher = (Teacher) _contactManager.Get(id);
            _contactManager.Delete(teacher);
            return RedirectToAction(nameof(Index), new { message = "delete"});
        }

        private bool TeacherExists(int id)
        {
            if (_contactManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        public ActionResult ExamDetails(int? id)
        {
            return RedirectToAction("Details", "Exam", new { id });
        }

        // Redirect to edit Question
        public ActionResult ExamEdit(int? id)
        {
            return RedirectToAction("Edit", "Exam", new { id });
        }

        private Teacher MapToTeacher(TeacherViewModel teacherViewModel)
        {
            return new Teacher(
                teacherViewModel.Id,
                teacherViewModel.FirstName,
                teacherViewModel.LastName,
                teacherViewModel.Email,
                teacherViewModel.PhoneNumber
                );
        }

        private TeacherViewModel MapToTeacherViewModel(Teacher teacher)
        {
            return new TeacherViewModel(
                teacher.Id,
                teacher.FirstName,
                teacher.LastName,
                teacher.FullName,
                teacher.Email,
                teacher.PhoneNumber,
                _examManager.GetExamssOfTeacher(teacher.Id)
                
                );
        }
    }
}