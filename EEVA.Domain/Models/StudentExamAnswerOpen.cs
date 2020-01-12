using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswerOpen : StudentExamAnswer, ICalculatePoints
    {

        public string Answer { get; set; }

        public QuestionOpen Question { get; set; }

        public StudentExamAnswerOpen()
        {

        }

        public StudentExamAnswerOpen(QuestionOpen question, StudentExam studentExam, string answer) : base(studentExam)
        {
            Answer = answer;
            Question = question;
        }

        public override int CalculatePoints()
        {
            QuestionOpen q = (QuestionOpen)Question;
            if (q.Answers != null)
            {
                List<AnswerOpen> list = (List<AnswerOpen>)q.Answers;
                int t = list.Count;
                int x = 0;
                foreach (AnswerOpen a in list)
                {
                    if (Answer.Contains(a.Keyword))
                    {
                        x++;
                    }
                }
                return x / t;
            }
            else return 0;
        }
    }
}
