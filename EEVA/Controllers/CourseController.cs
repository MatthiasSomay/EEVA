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
    public class CourseController : Controller
    {
        private readonly ExamManager _examManager;
        private readonly CourseManager _courseManager;
        private readonly ContactManager _contactManager;
        private readonly QuestionManager _questionManager;

        public CourseController(EEVAContext context)
        {
            _examManager = new ExamManager(context);
            _courseManager = new CourseManager(context);
            _contactManager = new ContactManager(context);
            _questionManager = new QuestionManager(context);
        }

        // GET: Course
        
        public IActionResult Index(string searchString, string currentFilter, int? pageNumber, bool courseCreated)
        {
            ViewBag.Message = courseCreated;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            List<CourseViewModel> courseViewModels = new List<CourseViewModel>();
            IEnumerable<Course> courses;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = _courseManager.Search(searchString);
            }
            else courses = _courseManager.GetAll();

            foreach (Course c in courses)
            {
                courseViewModels.Add(MapToCourseViewModel(c));
            }

            int pageSize = 8;
            return View(PaginatedList<CourseViewModel>.Create(courseViewModels, pageNumber ?? 1, pageSize));
        }

        // GET: Course/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseViewModel courseViewModel = MapToCourseViewModel(_courseManager.Get(id));

            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        [Authorize(Roles = "Teacher, Admin")]
        // GET: Course/Create
        public IActionResult Create()
        {
            return View(NewCourseViewModel());
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CourseName,CourseYear")] CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                Course course = MapToCourse(courseViewModel);
                _courseManager.Add(course);
                return RedirectToAction(nameof(Index), new { courseCreated = true});
            }
            return View(courseViewModel);
        }

        [Authorize(Roles = "Teacher, Admin")]
        // GET: Course/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course course = _courseManager.Get(id);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                CourseViewModel courseViewModel = MapToCourseViewModel(course);
                return View(courseViewModel);
            }
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CourseName,CourseYear")] CourseViewModel courseViewModel)
        {
            if (id != courseViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Course course = MapToCourse(courseViewModel);
                    _courseManager.Update(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(courseViewModel.Id))
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

        [Authorize(Roles = "Teacher, Admin")]
        // GET: Course/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CourseViewModel courseViewModel = MapToCourseViewModel(_courseManager.Get(id));

            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Course course = _courseManager.Get(id);
            _courseManager.Delete(course);
            return RedirectToAction(nameof(Index));
        }

        // Redirect to the related details of a Question
        public ActionResult QuestionDetails(int? id)
        {
            return RedirectToAction("Details", "Question", new { id });
        }

        // Redirect to edit Question
        public ActionResult QuestionEdit(int? id)
        {
            return RedirectToAction("Edit", "Question", new { id });
        }

        // Redirect to the related details of an Exam
        public ActionResult ExamDetails(int? id)
        {
            return RedirectToAction("Details", "Exam", new { id });
        }

        // Redirect to edit Exam
        public ActionResult ExamEdit(int? id)
        {
            return RedirectToAction("Edit", "Exam", new { id });
        }

        private bool CourseExists(int id)
        {
            if (_courseManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        //Mapping Course to CourseViewModel
        private CourseViewModel MapToCourseViewModel(Course c)
        {
            return new CourseViewModel(
                c.Id,
                c.CourseName,
                c.CourseYear,
                c.Questions,
                c.Exams);
        }

        //Mapping CourseViewModel to Course
        private Course MapToCourse(CourseViewModel courseViewModel)
        {
            return new Course(
                courseViewModel.Id,
                courseViewModel.CourseName,
                courseViewModel.CourseYear,
                courseViewModel.Questions,
                courseViewModel.Exams
                );
        }

        //Creating blank CourseViewModel
        private CourseViewModel NewCourseViewModel()
        {
            return new CourseViewModel();
        }
    }
}
