using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class AnswerMultipleChoiceViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public QuestionMultipleChoice QuestionMultipleChoice { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public bool IsAnswerCorrect { get; set; }

        public int questionId { get; set; }

        public AnswerMultipleChoiceViewModel(int id, QuestionMultipleChoice questionMultipleChoice, string answer, bool isAnswerCorrect)
        {
            Id = id;
            QuestionMultipleChoice = questionMultipleChoice;
            Answer = answer;
            IsAnswerCorrect = isAnswerCorrect;
        }

        public AnswerMultipleChoiceViewModel()
        {

        }
    }
}
