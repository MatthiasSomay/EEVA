using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class QuestionMultipleChoiceViewModel : QuestionViewModel
    {
 
        [Display(Name = "Answers")]
        public IEnumerable<AnswerMultipleChoice> Answers { get; set; }

        public QuestionMultipleChoiceViewModel(int id, string questionPhrase, IEnumerable<AnswerMultipleChoice> answers) : base(id, questionPhrase)
        {
            Answers = answers;
        }
    }
}
