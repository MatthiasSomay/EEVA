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

        public override int CalculatePoints()
        {
            QuestionOpen q = (QuestionOpen)Question;
            List<AnswerOpen> list = (List<AnswerOpen>)q.Answers;
            int t = list.Count;
            int x = 0;
            foreach (AnswerOpen a in list)
            {
                if (Answer.Contains(a.Keyword)){
                    x++;
                }
            }
            return x / t;
        }
    }
}
