using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class AnswerMultipleChoiceViewModel
    {
        public string Subtitle
        {
            get { return $"Details van '{Question.QuestionPhrase}'"; }
        }
        public Question Question { get; set; }
        public string Answer { get; set; }
    }
}
