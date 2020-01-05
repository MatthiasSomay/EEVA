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
    public class StudentController : Controller
    {
        private readonly ContactManager _contactManager;

        public StudentController(EEVAContext context)
        {
            _contactManager = new ContactManager(context);
        }

        [Authorize(Roles = "Teacher, Admin")]
        public ActionResult Index(string searchString, string currentFilter, int? pageNumber)
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
            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();
            IEnumerable<Contact> students;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = _contactManager.Search(searchString);
            }
            else students = _contactManager.GetAll();

            foreach (Contact c in students)
            {
                if (c is Student)
                {
                    Student student = (Student) c;
                    studentViewModels.Add(MapToStudentViewModel(student));
                }

            }

            int pageSize = 8;
            return View(PaginatedList<StudentViewModel>.Create(studentViewModels, pageNumber ?? 1, pageSize));
        }

        

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            StudentViewModel studentViewModel = MapToStudentViewModel((Student)_contactManager.Get(id));

            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View(new StudentViewModel());
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                Student student = MapToStudent(studentViewModel);
                _contactManager.Add(student);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Student student = (Student)_contactManager.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                StudentViewModel studentViewModel = MapToStudentViewModel(student);
                return View(studentViewModel);
            }
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Student student = MapToStudent(studentViewModel);
                    _contactManager.Update(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(studentViewModel.Id))
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

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            StudentViewModel studentViewModel = MapToStudentViewModel((Student)_contactManager.Get(id));

            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Student student = (Student)_contactManager.Get(id);
            _contactManager.Delete(student);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            if (_contactManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }


        private StudentViewModel MapToStudentViewModel(Student student)
        {
            return new StudentViewModel(
                student.Id,
                student.FirstName,
                student.LastName,
                student.FullName,
                student.Email,
                student.PhoneNumber);
        }

        private Student MapToStudent(StudentViewModel studentViewModel)
        {
            return new Student(
                studentViewModel.Id,
                studentViewModel.FirstName,
                studentViewModel.LastName,
                studentViewModel.Email,
                studentViewModel.PhoneNumber
                );
        }
    }
}
