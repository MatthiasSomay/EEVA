using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswerOpen : StudentExamAnswer, ICalculatePoints
    {

        public string Answer { get; set; }

        public StudentExamAnswerOpen()
        {

        }

        public StudentExamAnswerOpen(Question question, StudentExam studentExam, string answer) : base(question, studentExam)
        {
            Answer = answer;
        }

        public override double CalculatePoints()
        {
            QuestionOpen q = (QuestionOpen)Question;
            if (q.Answers != null)
            {
                double t = 0;
                double x = 0;
                foreach (AnswerOpen a in q.Answers)
                {
                    if (Answer.ToLower().Contains(a.Keyword.ToLower()))
                    {
                        x++;
                    }
                    t++;
                }
                return x / t;
            }
            else return 0;
        }
    }
}
