using EEVA.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EEVA.Web.Models
{
    public class QuestionOpenViewModel : QuestionViewModel
    {

        [Display(Name = "Keywords")]
        public IEnumerable<AnswerOpen> Answers { get; set; }

        public QuestionOpenViewModel(int id, string questionPhrase, List<AnswerOpen> answers) : base(id, questionPhrase)
        {
            Answers = answers;
        }
    }
}
