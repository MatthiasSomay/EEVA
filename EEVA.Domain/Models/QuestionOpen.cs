using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class QuestionOpen : Question
    {
        public IEnumerable<AnswerOpen> Answers { get; set; }

        public QuestionOpen()
        {

        }

        public QuestionOpen(int id, string questionPhrase, Course course, IEnumerable<AnswerOpen> answers) : base(id, questionPhrase, course)
        {
            Answers = answers;
        }

    }
}
