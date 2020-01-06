using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class QuestionMultipleChoice : Question
    {
        
        public IEnumerable<AnswerMultipleChoice> Answers { get; set; }

        public QuestionMultipleChoice()
        {

        }

        public QuestionMultipleChoice(int id, string questionPhrase, IEnumerable<AnswerMultipleChoice> answers) : base(id, questionPhrase)
        {
            Answers = answers;
        }
    }
}
