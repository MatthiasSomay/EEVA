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

        public StudentExamAnswerMultipleChoice(Question question, StudentExam studentExam, AnswerMultipleChoice answer) : base (question, studentExam)
        {
            Answer = answer;
        }
    }
}
