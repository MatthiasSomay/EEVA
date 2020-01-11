using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class StudentExamAnswerMultipleChoiceViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Question Question { get; set; }

        public AnswerMultipleChoice Answer { get; set; }

        public StudentExamAnswerMultipleChoiceViewModel()
        {

        }

        public StudentExamAnswerMultipleChoiceViewModel(int id, Question question, AnswerMultipleChoice answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }
    }
}
