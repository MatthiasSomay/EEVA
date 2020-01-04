using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class QuestionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Question Phrase")]
        [Required]
        public string QuestionPhrase { get; set; }

        public QuestionViewModel(int id, string questionPhrase)
        {
            Id = id;
            QuestionPhrase = questionPhrase;
        }
        public QuestionViewModel()
        {
            
        }
    }
}
