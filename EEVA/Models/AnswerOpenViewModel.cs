using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class AnswerOpenViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public QuestionOpen QuestionOpen { get; set; }

        [Required]
        public string Keyword { get; set; }

        public AnswerOpenViewModel(int id, QuestionOpen questionOpen, string keyword)
        {
            Id = id;
            QuestionOpen = questionOpen;
            Keyword = keyword;
        }

        public AnswerOpenViewModel()
        {

        }

    }
}
