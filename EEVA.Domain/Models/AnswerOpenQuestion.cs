using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.Models
{
    public class AnswerOpenQuestion : Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Answer Answer { get; set; }
        public List<string> Keywords { get; set; }

        public AnswerOpenQuestion()
        {

        }

        public AnswerOpenQuestion(Answer answer, List<string> keywords)
        {
            Answer = answer;
            Keywords = keywords;
        }
    }
}
