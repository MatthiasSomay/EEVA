using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswerMultipleChoice : StudentExamAnswer
    {
        public AnswerMultipleChoice Answer { get; set; }

        public StudentExamAnswerMultipleChoice()
        {

        }

        public StudentExamAnswerMultipleChoice(int id, Question question, AnswerMultipleChoice answer) : base (id, question)
        {
            Answer = answer;
        }
    }
}
