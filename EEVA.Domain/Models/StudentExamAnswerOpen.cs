using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswerOpen : StudentExamAnswer
    {

        public string Answer { get; set; }

        public StudentExamAnswerOpen()
        {

        }

        public StudentExamAnswerOpen(Question question, StudentExam studentExam, string answer) : base(question, studentExam)
        {
            Answer = answer;
        }
    }
}
