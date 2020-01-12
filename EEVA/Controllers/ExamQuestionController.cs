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
                    try
                    {
                        examQuestionViewModel.Answer = _studentExamAnswerManager.GetByStudentExamAndQuestionOpen(studentExam.Id, question.Id).Answer;
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (question is QuestionMultipleChoice)
                {
                    examQuestionViewModel.QuestionMultipleChoice = _questionManager.GetMultipleChoice(question.Id);
                    try
                    {
                       // examQuestionViewModel.Answers =_studentExamAnswerManager.GetByStudentExamAndQuestionMultiple(studentExam.Id, question.Id).Answer;
                    }
                    catch (Exception)
                    {

                        throw;
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
                    StudentExamAnswerOpen studentExamAnswerOpen = new StudentExamAnswerOpen(
                        question,
                        studentExam,
                        examQuestionViewModel.Answer
                        );

                    if(examQuestionViewModel.Answer == null)
                    {
                        _studentExamAnswerManager.Add(studentExamAnswerOpen);
                    }
                    else
                    {
                        StudentExamAnswerOpen so = _studentExamAnswerManager.GetByStudentExamAndQuestionOpen(studentExamId, question.Id);
                        _studentExamAnswerManager.Update(studentExamAnswerOpen, so);
                    }

                }
                else if (question is QuestionMultipleChoice)
                {                  
                   StudentExamAnswerMultipleChoice studentExamAnswerMultipleChoice = new StudentExamAnswerMultipleChoice(
                        question,
                        studentExam,
                        _answerMultipleChoiceManager.Get(examQuestionViewModel.Answers.FirstOrDefault())
                        );
                    if (examQuestionViewModel.Answer == null)
                    {
                        _studentExamAnswerManager.Add(studentExamAnswerMultipleChoice);
                    }
                    else
                    {
                        StudentExamAnswerMultipleChoice sc = _studentExamAnswerManager.GetByStudentExamAndQuestionMultiple(studentExamId, question.Id);
                        _studentExamAnswerManager.Update(studentExamAnswerMultipleChoice, sc);
                    }
                    _studentExamAnswerManager.Add(studentExamAnswerMultipleChoice);
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
