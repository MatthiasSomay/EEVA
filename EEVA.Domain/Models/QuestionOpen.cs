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

        public QuestionOpen(int id, string questionPhrase, IEnumerable<AnswerOpen> answers) : base(id, questionPhrase)
        {
            Answers = answers;
        }

    }
}
