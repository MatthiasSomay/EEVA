using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswerMultipleChoice : StudentExamAnswer , ICalculatePoints
    {
        public AnswerMultipleChoice Answer { get; set; }

        public StudentExamAnswerMultipleChoice()
        {

        }

        public StudentExamAnswerMultipleChoice(Question question, StudentExam studentExam, AnswerMultipleChoice answer) : base (question, studentExam)
        {
            Answer = answer;
        }

        public override int CalculatePoints()
        {
            QuestionMultipleChoice q = (QuestionMultipleChoice)Question;
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
