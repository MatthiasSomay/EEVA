using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswerMultipleChoice : StudentExamAnswer , ICalculatePoints
    {
        public AnswerMultipleChoice Answer { get; set; }

        public QuestionMultipleChoice Question { get; set; }

        public StudentExamAnswerMultipleChoice()
        {

        }

        public StudentExamAnswerMultipleChoice(QuestionMultipleChoice question, StudentExam studentExam, AnswerMultipleChoice answer) : base (studentExam)
        {
            Answer = answer;
            Question = question;
        }

        public override int CalculatePoints()
        {
            QuestionMultipleChoice q = Question;
            int x = 0;
            foreach (AnswerMultipleChoice a in q.Answers)
            {
                if(Answer.Id == a.Id && a.IsAnswerCorrect)
                {
                    x = 1;
                }
            }
            return x;
        }
    }
}
