using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class StudentExamAnswerOpenViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Question Question { get; set; }

        public string Answer { get; set; }

        public StudentExamAnswerOpenViewModel()
        {

        }

        public StudentExamAnswerOpenViewModel(int id, Question question, string answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }
    }
}
