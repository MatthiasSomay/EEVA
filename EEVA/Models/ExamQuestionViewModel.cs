using EEVA.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class ExamQuestionViewModel
    {
        public Course Course { get; set; }

        public string CourseName { get; set; }

        public Question Question { get; set; }

        public QuestionOpen QuestionOpen { get; set; }

        public QuestionMultipleChoice QuestionMultipleChoice { get; set; }

        public string QuestionPhrase { get; set; }

        public int QuestionNumber { get; set; }

        public StudentExam StudentExam { get; set; }

        public bool Answered { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        [BindProperty]
        public List<int> Answers { get; set; }

        public string Student { get; set; }

        [Display(Name = "Exam Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan EndTime { get; set; }


        public ExamQuestionViewModel(StudentExam studentExam, Question question, int questionNumber)
        {
            StudentExam = studentExam;
            Question = question;
            QuestionNumber = questionNumber;
            if(question != null)
            {
                QuestionPhrase = question.QuestionPhrase;
            }
            if (studentExam != null)
            {
                Course = studentExam.Exam.Course;
                CourseName = studentExam.Exam.Course.CourseName;
                Student = studentExam.Student.FullName;
                Date = studentExam.Exam.Date;
                EndTime = studentExam.Exam.EndTime;
                StartTime = StudentExam.Exam.StartTime;
            }
        }

        public ExamQuestionViewModel()
        {

        }
    }
}
