using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class QuestionMultipleChoice : Question
    {
        public List<AnswerMultipleChoice> Answers { get; set; }
    }
}
