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
    public class ExamQuestionController : Controller
    {
        private readonly QuestionManager _questionManager;
        private readonly CourseManager _courseManager;
        private readonly StudentExamManager _studentExamManager;
        private readonly StudentExamAnswerManager _studentExamAnswerManager;
        private readonly AnswerMultipleChoiceManager _answerMultipleChoiceManager;

        public ExamQuestionController(EEVAContext context)
        {
            _questionManager = new QuestionManager(context);
            _courseManager = new CourseManager(context);
            _studentExamManager = new StudentExamManager(context);
            _studentExamAnswerManager = new StudentExamAnswerManager(context);
            _answerMultipleChoiceManager = new AnswerMultipleChoiceManager(context);
        }

        // GET: ExamQuestions
        // Listing all the Questions related to a studentExam and providing the possibility to answer
        public IActionResult Index(int? pageNumber)
        {
            try
            {
                int? studentExamId = (int)TempData["studentExamId"];

                StudentExam studentExam = _studentExamManager.Get(studentExamId);
                List<ExamQuestionViewModel> examQuestionViewModels = new List<ExamQuestionViewModel>();
                int i = 0;
                foreach (Question q in studentExam.Exam.ExamQuestions)
                {
                    i++;
                    ExamQuestionViewModel examQuestionViewModel = NewExamQuestionViewModel(studentExam, q, i);
                    foreach (StudentExamAnswer a in examQuestionViewModel.StudentExam.StudentExamAnswers)
                    {
                        //Setting Question answered true if has exists
                        if (a.Question.Id == q.Id)
                        {
                            examQuestionViewModel.Answered = true;
                            break;
                        }
                        else examQuestionViewModel.Answered = false;
                    }
                    examQuestionViewModels.Add(examQuestionViewModel);
                }

                TempData["studentExamId"] = studentExam.Id;
                int pageSize = 8;
                return View(PaginatedList<ExamQuestionViewModel>.Create(examQuestionViewModels, pageNumber ?? 1, pageSize));

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Question/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question question = _questionManager.Get(id);

            if (question == null)
            {
                return NotFound();
            }
            else
            {
                switch (question.GetType().Name.ToString())
                {
                    case "QuestionOpen":
                        return RedirectToAction("Details", "QuestionOpen", new { id });
                    case "QuestionMultipleChoice":
                        return RedirectToAction("Details", "QuestionMultipleChoice", new { id });
                    default:
                        return NotFound();
                }
            }
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            return View(NewQuestionViewModel());
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,QuestionPhrase,CourseId")] QuestionViewModel QuestionViewModel)
        {
            if (ModelState.IsValid)
            {
                Question Question = MapToQuestion(QuestionViewModel);
                _questionManager.Add(Question);
                return RedirectToAction(nameof(Index), new { questionCreated = true });
            }
            return View();
        }

        // GET: Question/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question Question = _questionManager.Get(id);
            if (Question == null)
            {
                return NotFound();
            }
            else
            {
                QuestionViewModel QuestionViewModel = MapToQuestionViewModel(Question);
                return View(QuestionViewModel);
            }
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,QuestionPhrase")] QuestionViewModel QuestionViewModel)
        {
            if (id != QuestionViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Question Question = MapToQuestion(QuestionViewModel);
                    _questionManager.Update(Question);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(QuestionViewModel.Id))
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

        //Add a StudentExamAnswer
        public IActionResult Answer(int id)
        {
            try
            {
                int? studentExamId = (int)TempData["studentExamId"];

                StudentExam studentExam = _studentExamManager.Get(studentExamId);
                Question question = studentExam.Exam.ExamQuestions.ElementAt(id - 1);

                ExamQuestionViewModel examQuestionViewModel = NewExamQuestionViewModel(studentExam, question, id);


                if (question is QuestionOpen)
                {
                    examQuestionViewModel.QuestionOpen = _questionManager.GetOpen(question.Id);
                }
                else if (question is QuestionMultipleChoice)
                {
                    examQuestionViewModel.QuestionMultipleChoice = _questionManager.GetMultipleChoice(question.Id);
                }
                else
                {
                    return NotFound();
                }

                TempData["studentExamId"] = studentExam.Id;
                return View(examQuestionViewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Answer(int id, string submit, [Bind("Question,Answer,Answers")] ExamQuestionViewModel examQuestionViewModel)
        {
            try
            {
                int studentExamId = (int)TempData["studentExamId"];

                StudentExam studentExam = _studentExamManager.Get(studentExamId);
                Question question = studentExam.Exam.ExamQuestions.ElementAt(id - 1);

                if (question is QuestionOpen)
                {
                    StudentExamAnswerOpen studentExamAnswerOpen = new StudentExamAnswerOpen(
                        question,
                        studentExam,
                        examQuestionViewModel.Answer
                        );
                    _studentExamAnswerManager.Add(studentExamAnswerOpen);
                }
                else if (question is QuestionMultipleChoice)
                {                  
                   StudentExamAnswerMultipleChoice studentExamAnswerMultipleChoice = new StudentExamAnswerMultipleChoice(
                        question,
                        studentExam,
                        _answerMultipleChoiceManager.Get(examQuestionViewModel.Answers.FirstOrDefault())
                        );
                    _studentExamAnswerManager.Add(studentExamAnswerMultipleChoice);
                }
                else
                {
                    return NotFound();
                }
                TempData["studentExamId"] = studentExam.Id;

                switch (submit)
                {
                    case "Save and back to list":
                        return RedirectToAction(nameof(Index));
                    case "Next":
                        int increase = id + 1;
                        return RedirectToAction("Answer", "ExamQuestion", new { id = increase});
                    case "Previous":
                        int decrease = id - 1;
                        return RedirectToAction("Answer", "ExamQuestion", new { id = decrease });
                    default:
                        return NotFound();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //Review a StudentExamAnswer
        public IActionResult Review(int? id)
        {
            return RedirectToAction("ExamQuestionReview", "Question", new { id });
        }


        private bool QuestionExists(int id)
        {
            if (_questionManager.Get(id) != null)
            {
                return true;
            }
            else return false;
        }

        //New ExamQuestionViewModel
        private ExamQuestionViewModel NewExamQuestionViewModel(StudentExam studentExam, Question question, int questionNumber)
        {
            return new ExamQuestionViewModel(
                studentExam,
                question,
                questionNumber
                );
        }


        //Setting ExamQuestion
        private QuestionViewModel MapToQuestionViewModel(Question question)
        {
            return new QuestionViewModel(
                    question.Id,
                    question.QuestionPhrase,
                    question.Course,
                    _courseManager.GetAll()
            );
        }

        //Mapping Question to QuestionViewModel
        private Question MapToQuestion(QuestionViewModel questionViewModel)
        {
            return new Question(
                questionViewModel.Id,
                questionViewModel.QuestionPhrase,
                _courseManager.Get(questionViewModel.CourseId)
                );
        }

        //Creating blank QuestionViewModel
        private QuestionViewModel NewQuestionViewModel()
        {
            return new QuestionViewModel(_courseManager.GetAll());
        }
    }
}
