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

        public StudentExamAnswerOpen(int id, Question question, string answer) : base(id, question)
        {
            Answer = answer;
        }
    }
}
