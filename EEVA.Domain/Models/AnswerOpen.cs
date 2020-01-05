using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.Models
{
    public class AnswerOpen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public QuestionOpen QuestionOpen { get; set; }

        [Required]
        public string Keyword { get; set; }

        public AnswerOpen(int id, QuestionOpen questionOpen, string keyword)
        {
            Id = id;
            QuestionOpen = questionOpen;
            Keyword = keyword;
        }

        public AnswerOpen()
        {

        }

    }
}
