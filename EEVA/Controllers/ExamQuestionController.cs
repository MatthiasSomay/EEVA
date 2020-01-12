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

        //Add a StudentExamAnswer
        public IActionResult Answer(int id)
        {
            try
            {
                int? studentExamId = (int)TempData["studentExamId"];

                StudentExam studentExam = _studentExamManager.Get(studentExamId);
                Question question = studentExam.Exam.ExamQuestions.ElementAt(id - 1);

                ExamQuestionViewModel examQuestionViewModel = NewExamQuestionViewModel(studentExam, question, id);


                //Checking which type of question it is
                if (question is QuestionOpen)
                {
                    examQuestionViewModel.QuestionOpen = _questionManager.GetOpen(question.Id);
                    StudentExamAnswerOpen studentExamAnswerOpen = _studentExamAnswerManager.GetByStudentExamAndQuestionOpen(studentExam.Id, question.Id);
                    if (studentExamAnswerOpen != null)
                    {
                        examQuestionViewModel.Answer = studentExamAnswerOpen.Answer;
                    }
                }
                else if (question is QuestionMultipleChoice)
                {
                    examQuestionViewModel.QuestionMultipleChoice = _questionManager.GetMultipleChoice(question.Id);
                    StudentExamAnswerMultipleChoice studentExamAnswer = _studentExamAnswerManager.GetByStudentExamAndQuestionMultiple(studentExam.Id, question.Id);
                    if (studentExamAnswer != null)
                    {
                        List<int> answers = new List<int>();
                        answers.Add(studentExamAnswer.Answer.Id);
                        examQuestionViewModel.Answers = answers;
                    }
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

        //Add a StudentExamAnswer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Answer(int id, string submit, [Bind("Question,Answer,Answers")] ExamQuestionViewModel examQuestionViewModel)
        {
            try
            {
                int studentExamId = (int)TempData["studentExamId"];

                StudentExam studentExam = _studentExamManager.Get(studentExamId);
                Question question = studentExam.Exam.ExamQuestions.ElementAt(id - 1);

                //Checking which type of question it is
                if (question is QuestionOpen)
                {
                    StudentExamAnswerOpen so = _studentExamAnswerManager.GetByStudentExamAndQuestionOpen(studentExamId, question.Id);
                    StudentExamAnswerOpen studentExamAnswerOpen =
                                    new StudentExamAnswerOpen(
                                question,
                                studentExam,
                                examQuestionViewModel.Answer
                                );
                    if (so == null)
                    {
                        _studentExamAnswerManager.Add(studentExamAnswerOpen);
                    }
                    else
                    {
                        _studentExamAnswerManager.UpdateOpen(studentExamAnswerOpen, so);
                    }

                }
                else if (question is QuestionMultipleChoice)
                {
                    StudentExamAnswerMultipleChoice sc = _studentExamAnswerManager.GetByStudentExamAndQuestionMultiple(studentExamId, question.Id);

                    StudentExamAnswerMultipleChoice studentExamAnswerMultipleChoice =
                            new StudentExamAnswerMultipleChoice(
                         question,
                         studentExam,
                         _answerMultipleChoiceManager.Get(examQuestionViewModel.Answers.FirstOrDefault())
                         );

                    if (sc == null)
                    {
                        _studentExamAnswerManager.Add(studentExamAnswerMultipleChoice);
                    }
                    else
                    {
                        _studentExamAnswerManager.UpdateMultiple(studentExamAnswerMultipleChoice, sc);
                    }
                }
                else
                {
                    return NotFound();
                }

                TempData["studentExamId"] = studentExam.Id;

                //Checking which type of submit button is used
                switch (submit)
                {
                    case "Save and back to list":
                        return RedirectToAction(nameof(Index));
                    case "Next":
                        int increase = id + 1;
                        return RedirectToAction("Answer", "ExamQuestion", new { id = increase });
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

        public IActionResult SubmitExam(int? id)
        {
            return RedirectToAction("SubmitExam", "StudentExam", new { id });
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
    }
}
