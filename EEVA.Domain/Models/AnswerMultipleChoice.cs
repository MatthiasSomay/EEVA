using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.Models
{
    public class AnswerMultipleChoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public QuestionMultipleChoice QuestionMultipleChoice { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public bool IsAnswerCorrect { get; set; }

        public AnswerMultipleChoice(int id, QuestionMultipleChoice questionMultipleChoice, string answer, bool isAnswerCorrect)
        {
            Id = id;
            QuestionMultipleChoice = questionMultipleChoice;
            Answer = answer;
            IsAnswerCorrect = isAnswerCorrect;
        }

        public AnswerMultipleChoice()
        {
            
        }

    }
}
